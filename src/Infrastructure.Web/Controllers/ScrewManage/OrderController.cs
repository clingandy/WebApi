using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Enum;
using CrmScrew.Model.Search;
using CrmScrew.Model.View;
using Infrastructure.Common;
using Infrastructure.Web.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NPOI.SS.Formula;
using NPOI.XWPF.UserModel;

namespace Infrastructure.Web.Controllers.ScrewManage
{
    /// <summary>
    /// 订单管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        #region 构造

        private readonly IOrderRepository _repository;
        private readonly IDictRepository _repositoryDict;
        private readonly IClientRepository _repositoryClient;

        private readonly IMemoryCache _cache;

        /// <summary>
        /// 构造方法
        /// </summary>
        public OrderController(IOrderRepository repository, IClientRepository repositoryClient, IDictRepository repositoryDict, IMemoryCache cache)
        {
            _repository = repository;
            _repositoryClient = repositoryClient;
            _repositoryDict = repositoryDict;
            _cache = cache;
        }

        #endregion

        #region 商品

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<CrmProductScrewEntity>>))]
        public ActionResult<object> GetProductPageList([FromQuery]CrmProductScrewSearch search, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var data = _repository.GetProductScrewPageList(search, ref totalCount, pageIndex, pageSize);
            return data.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddProduct([FromForm]CrmProductScrewEntity model)
        {
            return _repository.AddProductScrew(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyProduct([FromForm]CrmProductScrewEntity model)
        {
            return _repository.ModifyProductScrew(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public ActionResult<object> DownloadProductTemplate()
        {
            //必须使用Path.Combine拼接，服务器不一定是windows
            return DownloadFile("产品导入模板.xlsx", Path.Combine("data","template","product.xlsx"));
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ImportProduct()
        {
            var filePath = Utils.FileSave(HttpContext, "xlsx");
            if (filePath.IsNullOrWhiteSpace())
            {
                return false.ResponseDataError("上传文件失败，格式不对或超过限制文件大小");
            }
            var errorMsgList = new List<string>();
            var table = NpoiExcelHelper.ExcelToTable(filePath);
            var importData = new List<CrmProductScrewEntity>();
            if (table.Rows.Count > 0)
            {
                var importDataTemp = NpoiExcelHelper.GetEntityFromDataTable<CrmProductScrewView>(table);
                var dictList = _cache.GetOrCreate("dictcontroller_getcrmdiclist", (entry) => _repositoryDict.GetDicList());      //类别id，缓存key和字典那边一样

                var index = 1;
                foreach (var item in importDataTemp)
                {
                    index++;
                    if(dictList.FirstOrDefault(t=> t.DictKey == item.ProductType && t.DictType == (int)CrmDictTypeEnum.产品类型) == null
                       || dictList.FirstOrDefault(t => t.PDictKey == item.ProductType && t.DictKey == item.ProductNameType && t.DictType == (int)CrmDictTypeEnum.产品名称) == null
                       || dictList.FirstOrDefault(t => t.DictKey == item.Material && t.DictType == (int)CrmDictTypeEnum.产品材质) == null
                       || dictList.FirstOrDefault(t => t.DictKey == item.Exterior && t.DictType == (int)CrmDictTypeEnum.产品外观) == null
                       || item.Specification.IsNullOrWhiteSpace()
                       || item.PackageWeight == 0
                       || item.ProposedPrice == 0
                       || item.RetailPrice == 0
                       || item.PurchasePrice == 0
                       || item.CostPrice == 0
                       )
                    {
                        errorMsgList.Add($"{index}行格式错误");
                        continue;   //跳出循环
                    }
                    var model = new CrmProductScrewEntity().CopyFrom(item);
                    model.ModifyTime = DateTime.Now;
                    model.Status = 1;
                    importData.Add(model);
                }
            }
            var isok = _repository.AddBatchProductScrew(importData);
            var msg = !isok ? "存入数据时失败" : errorMsgList.Count == 0 ? "全部导入成功" : "【部分失败】" + string.Join("|", errorMsgList);
            return isok.ResponseSuccessFailure(msg);
        }

        /// <summary>
        /// 查询商品历史信息
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<CrmProductScrewHistoryEntity>>))]
        public ActionResult<object> GetProductHistoryPageList(int productId, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var data = _repository.GetProductScrewHistoryPageList(productId, ref totalCount, pageIndex, pageSize);
            return data.ResponseSuccess("", totalCount);
        }

        #endregion

        #region 商品图片

        /// <summary>
        /// 返回全部商品图片
        /// </summary>
        /// <param name="productType">产品名称类型</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public ActionResult<object> GetProductImgAll(int? productType)
        {
            //类别id，缓存key和字典那边一样
            var dictAllList = _cache.GetOrCreate("dictcontroller_getcrmdiclist", (entry) => _repositoryDict.GetDicList());
            var imgs =_cache.GetOrCreate("ordercontroller_getcrmproductscrewimgall", (entry) =>  _repository.GetProductScrewImgAll());

            if (productType != null)
            {
                dictAllList = dictAllList.Where(t => t.PDictKey == productType).ToList();
            }
            var queryDict = dictAllList.Where(t => t.DictType == CrmDictTypeEnum.产品名称.GetEnumInt());

            var data = from dict in queryDict
                       join img in imgs on dict.DictKey equals img.ProductNameType into temp
                from tt in temp.DefaultIfEmpty()
                select new
                {
                    ProductName = dict.DictValue,
                    ProductNameType = dict.DictKey,
                    ProductScrewImgId = tt?.ProductScrewImgId ?? 0,
                    ImgUrl = tt?.ImgUrl ?? ""
                };

            return data.ResponseSuccess();
        }


        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddProductImg([FromForm]CrmProductScrewImgEntity model)
        {
            if (model.ImgUrl.IsNullOrWhiteSpace()) return false.ResponseDataError();
            var isOk = _repository.AddProductScrewImg(model);
            if (isOk)
            {
                _cache.Remove("ordercontroller_getcrmproductscrewimgall");
            }
            return isOk.ResponseSuccessFailure();
        }

        /// <summary>
        /// 修改商品图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyProductImg([FromForm]CrmProductScrewImgEntity model)
        {
            if (model.ImgUrl.IsNullOrWhiteSpace()) return false.ResponseDataError();
            var isOk = _repository.ModifyProductScrewImg(model);
            if (isOk)
            {
                _cache.Remove("ordercontroller_getcrmproductscrewimgall");
            }
            return isOk.ResponseSuccessFailure();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<string>))]
        public ActionResult<object> UploadProductImg()
        {
            var path = "uploadImg";
            var filePath = Utils.FileSave(HttpContext, "png|jpg", "img", path);
            if (filePath.IsNullOrWhiteSpace())
            {
                return false.ResponseDataError("上传文件失败，格式不对或超过限制文件大小");
            }
            filePath = filePath.Substring(filePath.IndexOf(path, StringComparison.Ordinal) + path.Length).Replace("\\","/");
            return filePath.ResponseSuccess();
        }
        

        #endregion

        #region 订单

        /// <summary>
        /// 查询订单 包含OrderItem
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<CrmOrderView>>))]
        public ActionResult<object> GetOrderPageList([FromQuery]CrmOrderSearch search, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var data = _repository.GetOrderPageList(search, ref totalCount, pageIndex, pageSize);
            return data.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<KeyValuePair<int, string>>>))]
        public ActionResult<object> GetOrderStatus()
        {
            return _repository.GetOrderStatus().Select(t=> new { t.Key, t.Value}).ResponseSuccess();
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyCrmOrder([FromForm]CrmOrderEntity model)
        {
            return _repository.ModifyCrmOrder(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 修改订单item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyCrmOrderItem([FromForm]CrmOrderItemEntity model)
        {
            return _repository.ModifyCrmOrderItem(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddCrmOrder([FromForm]AddOrderView order)
        {
            return _repository.AddCrmOrder(order).ResponseSuccessFailure();
        }

        /// <summary>
        /// 打印订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public ActionResult<object> PrintOrder(int orderId)
        {
            var order = _repository.GetCrmOrder(orderId);
            var orderItems = _repository.GetCrmOrderItemByOrderId(orderId).OrderBy(t=> t.OrderItemId).ToList();
            if (order == null || orderItems.Count == 0)
            {
                return false.ResponseDataError();
            }
            var client = _repositoryClient.GetClient(order.ClientId) ?? new CrmClientEntity();

            string filePath = Path.Combine(IOUtils.GetBaseDirectory(), "data", "template", "order.docx");
            using (FileStream filestream = new FileStream(filePath, FileMode.Open))
            {
                XWPFDocument doc = new XWPFDocument(filestream);
                //遍历表格
                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    var itable = tables.IndexOf(table);
                    if (itable == 0)
                    {
                        if (table.Rows.Count != 6) continue;
                        //公司和客户信息
                        for (int i = 0; i < 6; i++)
                        {
                            ReplaceClientCompanyKey(table.Rows[i], order, client);
                        }
                    }
                    else if(itable == 1)
                    {
                        if (table.Rows.Count != 3) continue;
                        //数据列表信息
                        var temRow01 = table.GetRow(1);   //数据模板行
                        var temRow02 = table.GetRow(2);   //表格统计行
                        table.RemoveRow(2);     //移除统计行
                        table.RemoveRow(1);     //移除模板行

                        //订单item
                        var index = 1;
                        foreach (var item in orderItems)
                        {
                            //创建复制行属性
                            var addRow = table.CreateRow();
                            XWPFTableRowCopy(addRow, temRow01);
                            ReplaceItemKey(addRow, item, index);
                            index++;
                        }

                        //运费
                        var freightPriceRow = table.CreateRow();
                        XWPFTableRowCopy(freightPriceRow, temRow01);
                        ReplaceOrderKey(freightPriceRow, order, ++index);
                        //统计
                        table.CreateRow();
                        table.CreateRow();
                        ReplaceOrderKey(temRow02, order, ++index);
                        table.AddRow(temRow02);
                    }                   
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Write(ms);
                    // 不能直接ms.GetBuffer(), 长度不对少几个； 生成的docx打开会提示错误，恢复后才行
                    var fileBuffer = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(fileBuffer, 0, (int)ms.Length);
                    return DownloadFile($"订单【{order.OrderId}】打印.docx",fileBuffer);
                }
            }
            
        }

        /// <summary>
        /// 替换公司和客户标签
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        /// <param name="client"></param>
        private void ReplaceClientCompanyKey(XWPFTableRow row, CrmOrderEntity item, CrmClientEntity client)
        {
            foreach (var cell in row.GetTableCells())
            {
                var text = cell.GetText().Trim();
                if(text.IsNullOrWhiteSpace())continue;
                switch (text)
                {
                    case "{Company:Name}":
                        text = text.Replace(text, AppSettingsHelper.GetString("Company:Name"));
                        break;
                    case "{Company:Address}":
                        text = text.Replace(text, AppSettingsHelper.GetString("Company:Address"));
                        break;
                    case "{Company:Tel}":
                        text = text.Replace(text, AppSettingsHelper.GetString("Company:Tel"));
                        break;
                    case "制单人：{Company: Lister}":
                        text = text.Replace("{Company: Lister}", AppSettingsHelper.GetString("Company:Lister"));
                        break;
                    case "NO. {dateTime}":
                    case "发货日期：{dateTime}":
                        text = text.Replace("{dateTime}", DateTime.Today.ToString("yyyy-MM-dd"));
                        break;
                    case "客户名称：{clientName}":
                        text = text.Replace("{clientName}", client.ClientName);
                        break;
                    case "联系人：{clientRealName}":
                        text = text.Replace("{clientRealName}", client.RealName ?? client.ClientName);
                        break;
                    case "客户地址：{clientAddress}":
                        var addr = Regex.Replace(item.FullAddress, @"[1]+\d{10}", ""); //手机
                        addr = Regex.Replace(addr, @"(\d{3,4}-)?\d{6,8}$", "");    //座机
                        text = text.Replace("{clientAddress}", addr.Replace(" ",""));
                        break;
                    case "电话：{clientTel}":
                        var tel = Regex.Match(item.FullAddress, @"[1]+\d{10}").Value;       //手机
                        if (tel.IsNullOrWhiteSpace())
                        {
                            tel = Regex.Match(item.FullAddress, @"(\d{3,4}-)?\d{6,8}$").Value;     //座机
                        }
                        text = text.Replace("{clientTel}", tel);
                        break;
                }

                // 为保留模板的格式
                foreach (var para in cell.Paragraphs)
                {
                    for (var i = 0; i < para.Runs.Count; i++)
                    {
                        para.Runs[i].SetText(i == para.Runs.Count - 1 ? text : "", 0);
                    }
                }
            }
        }

        /// <summary>
        /// 替换订单标签
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        private void ReplaceOrderKey(XWPFTableRow row, CrmOrderEntity item, int index)
        {
            foreach (var cell in row.GetTableCells())
            {
                var text = cell.GetText().Trim();
                switch (text)
                {
                    case "金额合计（大写）：{allTotalPriceBig}":
                        text = text.Replace("{allTotalPriceBig}", item.TotlaPrice.ToString("N2").MoneyToChinese());
                        break;
                    case "小写金额：{allTotalPriceSmall}":
                        text = text.Replace("{allTotalPriceSmall}", item.TotlaPrice.ToString("N2"));
                        break;

                    case "{index}":
                        text = text.Replace(text, index.ToString());
                        break;
                    case "{productId}":
                        text = text.Replace(text, "运费");
                        break;
                    case "{totalPrice}":
                        text = text.Replace(text, item.FreightPrice.ToString("N2"));
                        break;
                    case "{productFullName}":
                    case "{unit}":
                    case "{count}":
                    case "{singlePrice}":
                    case "{remark}":
                        text = text.Replace(text, "");
                        break;
                }

                // 为保留模板的格式
                foreach (var para in cell.Paragraphs)
                {
                    for (var i = 0; i < para.Runs.Count; i++)
                    {
                        para.Runs[i].SetText(i == para.Runs.Count - 1 ? text : "", 0);
                    }
                }
            }
        }

        /// <summary>
        /// 替换订单item标签
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        private void ReplaceItemKey(XWPFTableRow row, CrmOrderItemEntity item, int index)
        {
            foreach (var cell in row.GetTableCells())
            {
                var text = cell.GetText();
                switch (text)
                {
                    case "{index}":
                        text = text.Replace(text, index.ToString());
                        break;
                    case "{productId}":
                        text = text.Replace(text, item.ProductId.GetHashCode().ToString());
                        break;
                    case "{productFullName}":
                        text = text.Replace(text, item.ProductFullName);
                        break;
                    case "{unit}":
                        text = text.Replace(text, "PCS");
                        break;
                    case "{count}":
                        text = text.Replace(text, item.Count.ToString());
                        break;
                    case "{singlePrice}":
                        text = text.Replace(text, item.SinglePrice.ToString("N2"));
                        break;
                    case "{totalPrice}":
                        text = text.Replace(text, item.TotalPrice.ToString("N2"));
                        break;
                    case "{remark}":
                        text = text.Replace(text, "");
                        break;
                }

                // 为保留模板的格式
                foreach (var para in cell.Paragraphs)
                {
                    for (var i = 0; i < para.Runs.Count; i++)
                    {
                        para.Runs[i].SetText(i == para.Runs.Count - 1 ? text : "", 0);
                    }
                }
            }
        }

        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="sourceRow"></param>
        private void XWPFTableRowCopy(XWPFTableRow targetRow, XWPFTableRow sourceRow)
        {
            targetRow.GetCTRow().trPr = sourceRow.GetCTRow().trPr;
            List<XWPFTableCell> cellList = sourceRow.GetTableCells();
            //复制列及其属性和内容
            foreach (var sourceCell in cellList)
            {
                var index = cellList.IndexOf(sourceCell);
                var targetCell = targetRow.GetCell(index);
                if(targetCell == null) break;
                targetCell.GetCTTc().tcPr = sourceCell.GetCTTc().tcPr;
                if (sourceCell.Paragraphs != null && sourceCell.Paragraphs.Count > 0)
                {
                    //设置段落样式
                    var sourceParagraph = sourceCell.Paragraphs[0];
                    targetCell.Paragraphs[0].Alignment = ParagraphAlignment.CENTER;
                    if (sourceParagraph.Runs != null && sourceParagraph.Runs.Count > 0)
                    {
                        var cellR = targetCell.Paragraphs[0].CreateRun();
                        cellR.SetText(sourceCell.GetText());
                        cellR.IsBold = sourceParagraph.Runs[0].IsBold;
                        //cellR.FontFamily = sourceParagraph.Runs[0].FontFamily;
                        cellR.FontSize = 10;
                    }
                    else
                    {
                        targetRow.GetCell(index).SetText(sourceCell.GetText());
                    }
                }
                else
                {
                    targetRow.GetCell(index).SetText(sourceCell.GetText());
                }
            }
        }

        #endregion
    }
}
