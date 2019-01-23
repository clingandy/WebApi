using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Enum;
using CrmScrew.Model.Search;
using CrmScrew.Model.View;
using Infrastructure.Common;
using Infrastructure.Web.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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

        private readonly IMemoryCache _cache;

        /// <summary>
        /// 构造方法
        /// </summary>
        public OrderController(IOrderRepository repository, IDictRepository repositoryDict, IMemoryCache cache)
        {
            _repository = repository;
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
        public ActionResult DownloadProductTemplate()
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

        #endregion
    }
}
