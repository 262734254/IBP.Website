-- exec lrs_CodeNameGetByTypeNo @CodeType=0

truncate table ibp_db.dbo.custom_data_info;
truncate table ibp_db.dbo.custom_data_value;

DECLARE @custom_data_id varchar(50);
select @custom_data_id = newid();

-- �ͻ��ȼ� ----------------------------------------------------------
INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�ͻ��ȼ�',Null,'custom_data','customer_level','string',2,20,1,0,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 0;


-- �ͻ���Դ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�ͻ���Դ',Null,'custom_data','customer_level','string',2,20,1,1,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 1;


-- �������� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��������',Null,'custom_data','customer_level','string',2,20,1,2,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 2;


-- �������� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��������',Null,'custom_data','customer_level','string',2,20,1,3,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 3;
	
	

-- ֤������ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'֤������',Null,'custom_data','customer_level','string',2,20,1,4,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 4;
	
	
	
-- ��Ӫ�� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��Ӫ��',Null,'custom_data','customer_level','string',2,20,1,5,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 5;
	
-- ������ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'������',Null,'custom_data','customer_level','string',2,20,1,6,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 6;
	
-- �������� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��������',Null,'custom_data','customer_level','string',2,20,1,7,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 7;

-- �������� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��������',Null,'custom_data','customer_level','string',2,20,1,8,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 8;	
		

-- �����쳣���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�����쳣����',Null,'custom_data','customer_level','string',2,20,1,9,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 10;	

-- �����쳣���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�����쳣����',Null,'custom_data','customer_level','string',2,20,1,10,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 11;	

-- �����쳣���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�����쳣����',Null,'custom_data','customer_level','string',2,20,1,11,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 12;				
			
-- �����쳣���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�����쳣����',Null,'custom_data','customer_level','string',2,20,1,12,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 13;		
				
-- ǩ���쳣���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'ǩ���쳣����',Null,'custom_data','customer_level','string',2,20,1,13,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 14;		
				
-- �����쳣���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�����쳣����',Null,'custom_data','customer_level','string',2,20,1,14,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 15;					

-- ���͹�˾ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'���͹�˾',Null,'custom_data','customer_level','string',2,20,1,15,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 16;		
					
-- �㲥���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�㲥����',Null,'custom_data','customer_level','string',2,20,1,16,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 17;	
	
	
-- ��������ԭ�� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��������ԭ��',Null,'custom_data','customer_level','string',2,20,1,17,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 18;	
	
-- ���۳���ԭ�� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'���۳���ԭ��',Null,'custom_data','customer_level','string',2,20,1,18,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 19;	
	
-- ���뼶�� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'���뼶��',Null,'custom_data','customer_level','string',2,20,1,19,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 20;	
		
-- ����ʧ��ԭ�� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'����ʧ��ԭ��',Null,'custom_data','customer_level','string',2,20,1,20,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 21;		


-- ������Դ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'������Դ',Null,'custom_data','customer_level','string',2,20,1,21,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 22;	

-- ͨѶ���� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'ͨѶ����',Null,'custom_data','customer_level','string',2,20,1,22,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 24;	
								

-- ��ѡƷ�� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'��ѡƷ��',Null,'custom_data','customer_level','string',2,20,1,23,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 25;	
	
-- �ֻ���λ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'�ֻ���λ',Null,'custom_data','customer_level','string',2,20,1,24,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 26;	
		
-- ���۳��� ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'���۳���',Null,'custom_data','customer_level','string',2,20,1,25,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 27;	
	

-- ����״̬ ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'����״̬',Null,'custom_data','customer_level','string',2,20,1,26,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('8FA195F5-21B4-45D8-8472-23E013B336BA',@custom_data_id,'����','WorkStatus001',0,0,GETDATE(),'admin',null,null,0),
	('DD1C5D4E-46F9-4535-8462-661534DC2A1A',@custom_data_id,'ת��','WorkStatus002',1,0,GETDATE(),'admin',null,null,0),
	('2CA85927-4E1E-408A-8E44-A3A24F1046D5',@custom_data_id,'��ְ','WorkStatus003',2,0,GETDATE(),'admin',null,null,0);
											
select c.data_name, v.* from ibp_db.dbo.custom_data_value v inner join ibp_db.dbo.custom_data_info c on v.data_id = C.data_id where v.data_id = @custom_data_id order by v.sort_order 


GO

-- select NEWID()

-- select * from Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 0;