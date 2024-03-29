--  -------------------------------------------------- 
--  Generated by Enterprise Architect Version 8.0.858
--  Created On : 星期一, 02 一月, 2012 
--  DBMS       : SQL Server 2008 
--  -------------------------------------------------- 

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('product_info') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE product_info
;

CREATE TABLE product_info ( 
	product_id varchar(50) NOT NULL,    -- 主键ID 
	category_id varchar(50) NOT NULL,    -- 类别ID 
	product_code varchar(50) NOT NULL,    -- 产品编号 
	product_name varchar(50) NOT NULL,    -- 产品名称 
	sales_status varchar(50) NOT NULL,    -- 销售状态 
	status int,    -- 状态 
	created_on datetime NOT NULL,
	created_by varchar(50),
	modified_on datetime,
	modified_by varchar(50),
	status_code int
)
;

ALTER TABLE product_info ADD CONSTRAINT PK_product_info 
	PRIMARY KEY CLUSTERED (product_id)
;

EXEC sp_addextendedproperty 'MS_Description', '产品信息表', 'Schema', dbo, 'table', product_info
;
EXEC sp_addextendedproperty 'MS_Description', '主键ID', 'Schema', dbo, 'table', product_info, 'column', product_id
;

EXEC sp_addextendedproperty 'MS_Description', '类别ID', 'Schema', dbo, 'table', product_info, 'column', category_id
;

EXEC sp_addextendedproperty 'MS_Description', '产品编号', 'Schema', dbo, 'table', product_info, 'column', product_code
;

EXEC sp_addextendedproperty 'MS_Description', '产品名称', 'Schema', dbo, 'table', product_info, 'column', product_name
;

EXEC sp_addextendedproperty 'MS_Description', '销售状态', 'Schema', dbo, 'table', product_info, 'column', sales_status
;

EXEC sp_addextendedproperty 'MS_Description', '状态', 'Schema', dbo, 'table', product_info, 'column', status
;






