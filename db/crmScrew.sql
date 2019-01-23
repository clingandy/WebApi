-- --------------------------------------------------------
-- 主机:                           47.99.217.95
-- 服务器版本:                        5.5.60-MariaDB - MariaDB Server
-- 服务器操作系统:                      Linux
-- HeidiSQL 版本:                  9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出 crm_screw 的数据库结构
CREATE DATABASE IF NOT EXISTS `crm_screw` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `crm_screw`;


-- 导出  表 crm_screw.crm_client 结构
CREATE TABLE IF NOT EXISTS `crm_client` (
  `clientId` int(11) NOT NULL AUTO_INCREMENT,
  `clientName` varchar(50) NOT NULL,
  `sex` bit(1) NOT NULL DEFAULT b'1' COMMENT '1男0女',
  `age` tinyint(4) NOT NULL DEFAULT '0',
  `weChat` varchar(50) DEFAULT '',
  `qq` varchar(50) DEFAULT '',
  `mobile` varchar(20) NOT NULL,
  `modifyTime` datetime NOT NULL,
  PRIMARY KEY (`clientId`),
  KEY `clientName` (`clientName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='客户表';

-- 正在导出表  crm_screw.crm_client 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_client` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_client` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_client_address 结构
CREATE TABLE IF NOT EXISTS `crm_client_address` (
  `clientAddressId` int(11) NOT NULL AUTO_INCREMENT,
  `clientId` int(11) NOT NULL,
  `fullAddress` varchar(100) NOT NULL,
  `mobile` varchar(20) NOT NULL,
  `address` varchar(100) DEFAULT NULL,
  `provinceId` int(11) DEFAULT NULL,
  `provinceName` varchar(50) DEFAULT NULL,
  `cityId` int(11) DEFAULT NULL,
  `cityName` varchar(50) DEFAULT NULL,
  `areaId` int(11) DEFAULT NULL,
  `areaName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`clientAddressId`),
  KEY `clientId` (`clientId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  crm_screw.crm_client_address 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_client_address` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_client_address` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_dict 结构
CREATE TABLE IF NOT EXISTS `crm_dict` (
  `dictId` int(11) NOT NULL AUTO_INCREMENT,
  `dictType` int(11) NOT NULL DEFAULT '0' COMMENT '类型多个的需要分类（对应枚举）',
  `dictkey` int(11) NOT NULL DEFAULT '0',
  `pDictKey` int(11) NOT NULL DEFAULT '0',
  `dictValue` varchar(50) NOT NULL,
  `sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `status` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`dictId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT COMMENT='字典表';

-- 正在导出表  crm_screw.crm_dict 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_dict` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_dict` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_order 结构
CREATE TABLE IF NOT EXISTS `crm_order` (
  `orderId` int(11) NOT NULL AUTO_INCREMENT,
  `clientId` int(11) NOT NULL COMMENT '客户ID',
  `status` tinyint(4) NOT NULL COMMENT '状态（1未付款2已付款3未发货4已发货5交易成功6交易关闭）',
  `shippingType` tinyint(4) NOT NULL COMMENT '物流类型',
  `shippingCode` varchar(50) DEFAULT NULL COMMENT '快递编码',
  `freightPrice` decimal(8,2) NOT NULL COMMENT '运费',
  `totlaPrice` decimal(8,2) NOT NULL COMMENT '总价格',
  `fullAddress` varchar(100) NOT NULL COMMENT '收货地址',
  `description` varchar(200) DEFAULT NULL COMMENT '描述、备注',
  `createTime` datetime NOT NULL COMMENT '创建日期',
  `paymentTime` datetime DEFAULT NULL COMMENT '支付时间',
  `consignTime` datetime DEFAULT NULL COMMENT '发货时间',
  `endTime` datetime DEFAULT NULL COMMENT '完成时间',
  `closeTime` datetime DEFAULT NULL COMMENT '关闭时间',
  PRIMARY KEY (`orderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='订单表';

-- 正在导出表  crm_screw.crm_order 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_order` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_order` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_order_item 结构
CREATE TABLE IF NOT EXISTS `crm_order_item` (
  `orderItemId` int(11) NOT NULL AUTO_INCREMENT,
  `productId` int(11) NOT NULL DEFAULT '0' COMMENT '商品ID',
  `orderId` int(11) NOT NULL COMMENT '订单ID',
  `productFullName` varchar(100) NOT NULL COMMENT '商品全称',
  `count` int(11) NOT NULL COMMENT '订单数量',
  `weight` decimal(8,3) NOT NULL COMMENT '订单重量（单位：KG）',
  `singlePrice` decimal(8,2) NOT NULL COMMENT '单价',
  `totalPrice` decimal(8,2) NOT NULL COMMENT '总价格',
  `productUrl` varchar(100) DEFAULT NULL COMMENT '商品地址',
  `imgUrl` varchar(100) DEFAULT NULL COMMENT '图片地址',
  PRIMARY KEY (`orderItemId`),
  KEY `orderId` (`orderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='订单详情';

-- 正在导出表  crm_screw.crm_order_item 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_order_item` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_order_item` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_product_screw 结构
CREATE TABLE IF NOT EXISTS `crm_product_screw` (
  `productId` int(11) NOT NULL AUTO_INCREMENT,
  `productType` tinyint(4) NOT NULL,
  `productNameType` tinyint(4) NOT NULL,
  `specification` varchar(20) NOT NULL COMMENT '规格',
  `material` tinyint(4) NOT NULL COMMENT '材质',
  `exterior` tinyint(4) NOT NULL COMMENT '外观 表面处理',
  `size` tinyint(4) NOT NULL COMMENT '大小（单位MM）',
  `thickness` tinyint(4) NOT NULL COMMENT '厚度（单位MM）',
  `weight` decimal(7,3) NOT NULL COMMENT '重量（单位KG）',
  `packageWeight` decimal(7,3) NOT NULL COMMENT '包重量（单位KG）',
  `proposedPrice` decimal(8,2) NOT NULL COMMENT '参考价格',
  `retailPrice` decimal(8,2) NOT NULL COMMENT '零售价格',
  `purchasePrice` decimal(8,2) NOT NULL COMMENT '进货价格',
  `costPrice` decimal(8,2) NOT NULL COMMENT '成本价格',
  `modifyTime` datetime NOT NULL COMMENT '修改时间',
  `imgUrl` varchar(100) DEFAULT NULL COMMENT '图片地址',
  `status` tinyint(4) NOT NULL COMMENT '1上架2下架',
  PRIMARY KEY (`productId`),
  KEY `productType` (`productType`),
  KEY `productNameType` (`productNameType`),
  KEY `specification` (`specification`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='产品信息';

-- 正在导出表  crm_screw.crm_product_screw 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_product_screw` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_product_screw` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_product_screw_history 结构
CREATE TABLE IF NOT EXISTS `crm_product_screw_history` (
  `pHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `productId` int(11) NOT NULL,
  `productType` tinyint(4) NOT NULL COMMENT '产品类型',
  `productNameType` tinyint(4) NOT NULL COMMENT '产品名称',
  `specification` varchar(20) NOT NULL COMMENT '规格',
  `material` tinyint(4) NOT NULL COMMENT '材质',
  `exterior` tinyint(4) NOT NULL COMMENT '外观 表面处理',
  `size` tinyint(4) NOT NULL COMMENT '大小（单位MM）',
  `thickness` tinyint(4) NOT NULL COMMENT '厚度（单位MM）',
  `weight` decimal(7,3) NOT NULL COMMENT '重量（单位KG）',
  `packageWeight` decimal(7,3) NOT NULL COMMENT '包重量（单位KG）',
  `proposedPrice` decimal(8,2) NOT NULL COMMENT '参考价格',
  `retailPrice` decimal(8,2) NOT NULL COMMENT '零售价格',
  `purchasePrice` decimal(8,2) NOT NULL COMMENT '进货价格',
  `costPrice` decimal(8,2) NOT NULL COMMENT '成本价格',
  `modifyTime` datetime NOT NULL COMMENT '修改时间',
  `imgUrl` varchar(100) DEFAULT NULL COMMENT '图片地址',
  `status` tinyint(4) NOT NULL COMMENT '1上架2下架',
  PRIMARY KEY (`pHistoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT COMMENT='产品信息';

-- 正在导出表  crm_screw.crm_product_screw_history 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_product_screw_history` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_product_screw_history` ENABLE KEYS */;


-- 导出  表 crm_screw.crm_product_screw_img 结构
CREATE TABLE IF NOT EXISTS `crm_product_screw_img` (
  `productScrewImgId` int(11) NOT NULL AUTO_INCREMENT,
  `productNameType` int(11) NOT NULL COMMENT '产品名称类型',
  `imgUrl` varchar(100) NOT NULL COMMENT '地址',
  PRIMARY KEY (`productScrewImgId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='产品图片';

-- 正在导出表  crm_screw.crm_product_screw_img 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `crm_product_screw_img` DISABLE KEYS */;
/*!40000 ALTER TABLE `crm_product_screw_img` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
