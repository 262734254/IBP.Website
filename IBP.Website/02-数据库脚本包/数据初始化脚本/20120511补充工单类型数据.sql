update [workorder_result_info] set sort_order = 3 where workorder_result_id = 'C4533264-B571-4F97-85B9-54B554B81125';
update [workorder_result_info] set sort_order = 2 where workorder_result_id = 'F44FA324-8645-4285-83FE-D2B376646033';


delete from [workorder_type_info] where [workorder_type_id] = '7E757BCC-8813-4AA9-AF5A-537E144DA51A';
delete from [workorder_type_info] where [workorder_type_id] = '9100AE58-ED64-4F9F-866D-3A4D37EEF41C';
delete from [workorder_type_info] where [workorder_type_id] = 'B6A9F553-3539-4203-AD94-65B6BDB41E24';

delete from [workorder_status_info] where [workorder_type_id] = '7E757BCC-8813-4AA9-AF5A-537E144DA51A';
delete from [workorder_status_info] where [workorder_type_id] = '9100AE58-ED64-4F9F-866D-3A4D37EEF41C';
delete from [workorder_status_info] where [workorder_type_id] = 'B6A9F553-3539-4203-AD94-65B6BDB41E24';

delete from [workorder_result_info] where [workorder_type_id] = '7E757BCC-8813-4AA9-AF5A-537E144DA51A';
delete from [workorder_result_info] where [workorder_type_id] = '9100AE58-ED64-4F9F-866D-3A4D37EEF41C';
delete from [workorder_result_info] where [workorder_type_id] = 'B6A9F553-3539-4203-AD94-65B6BDB41E24';


INSERT INTO [workorder_type_info]
           ([workorder_type_id],[type_name],[description],[workorder_type_status],[sort_order],[created_on],[created_by],[status_code])
VALUES
		   ('7E757BCC-8813-4AA9-AF5A-537E144DA51A','话费赠送','话费赠送',0,0,getdate(),'Admin',0),
		   ('9100AE58-ED64-4F9F-866D-3A4D37EEF41C','托收激活','托收激活',0,0,getdate(),'Admin',0),
		   ('B6A9F553-3539-4203-AD94-65B6BDB41E24','我的营销','我的营销',0,0,getdate(),'Admin',0);

-- 插入 话费赠送 工单状态 -----------------
INSERT INTO [workorder_status_info]
           ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
		newid(),'7E757BCC-8813-4AA9-AF5A-537E144DA51A',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]
from 
	workorder_status_info 
where 
	workorder_type_id = '29807854-4C2C-4D4C-A3C7-9585812C7629' order by sort_order;
	
-- 插入 托收激活 工单状态 -----------------
INSERT INTO [workorder_status_info]
           ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
		newid(),'9100AE58-ED64-4F9F-866D-3A4D37EEF41C',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]
from 
	workorder_status_info 
where 
	workorder_type_id = '29807854-4C2C-4D4C-A3C7-9585812C7629' order by sort_order;
	
-- 插入 我的营销 工单状态 -----------------
INSERT INTO [workorder_status_info]
           ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
		newid(),'B6A9F553-3539-4203-AD94-65B6BDB41E24',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]
from 
	workorder_status_info 
where 
	workorder_type_id = '29807854-4C2C-4D4C-A3C7-9585812C7629' order by sort_order;
	

-- 插入 托收激活 工单结果 -----------------

INSERT INTO [workorder_result_info]
           ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'9100AE58-ED64-4F9F-866D-3A4D37EEF41C',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0
from
	[workorder_result_info]
where
	workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629'
order by 
	sort_order;
	
-- 插入 我的营销 工单结果 -----------------

INSERT INTO [workorder_result_info]
           ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'B6A9F553-3539-4203-AD94-65B6BDB41E24',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0
from
	[workorder_result_info]
where
	workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629'
order by 
	sort_order;
	
-- 插入 我的营销 工单结果 -----------------

INSERT INTO [workorder_result_info]
           ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	(NEWID(),'7E757BCC-8813-4AA9-AF5A-537E144DA51A','跟进','跟进',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
	(NEWID(),'7E757BCC-8813-4AA9-AF5A-537E144DA51A','完成','完成',0,1,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
	(NEWID(),'7E757BCC-8813-4AA9-AF5A-537E144DA51A','结束','结束',0,2,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);

delete from workorder_result_info where result_name = '未处理';

INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('D27D62DB-D439-4CFB-A617-7B1A7B0248BC','097F1B35-F8A4-4C75-BFEE-EEDB3FAD458B',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('2730BC45-4464-487F-A3E2-3C0C99AB1B82','1A9D6EAA-28C3-4258-8E54-BCF4EBA8B7F1',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('3476678E-1F49-4A10-A15C-85967892E2CA','2403E962-1A50-4925-9F78-D43623598016',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('D1485CB2-2EF3-46A8-8E3C-CA53477E2456','29807854-4C2C-4D4C-A3C7-9585812C7629',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('00E8A523-D32C-4A6A-8482-EF5B5E985D45','2EB02087-F4AE-4056-B2E7-5B1E471E8775',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('60080F90-7300-4CDA-8804-E53B8287A84F','3963A333-1CC1-4D5F-AC3A-90B26710470E',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('30ABFC04-8527-4709-B829-CD0386AB3B50','6A36D5EF-DC4C-4EAF-B06C-64E6AC9C4B2C',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('A8627EEC-6B97-423B-882E-BF0816680950','6E11C6CE-C5A7-4748-9C34-F8DB49BD15B8',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('916E1102-0CF6-47D8-9B9A-1A444DDF959A','7DB5630A-6109-4A69-AF9A-1A1049164365',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('B56054B9-1F2F-48AE-A8A8-C11529A38FB5','7E757BCC-8813-4AA9-AF5A-537E144DA51A',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('0B85644F-418A-41F1-8A83-E244A8DA948A','82A5F0F9-36CA-4F94-8325-DAAC77600541',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('B6C3726B-3702-4223-9DDF-AF7C5BCA4D5D','9100AE58-ED64-4F9F-866D-3A4D37EEF41C',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('9984C772-016E-4EDD-9B0C-B14EB07A2207','9F71857F-BCF6-41E7-A6D3-AEEB8F64E851',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('6F062C1F-8DEA-4B8D-B145-6EA0AC7CCE0E','A4D34225-E07D-4665-8BA5-3D8456B7C5CA',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('92BFCABD-7A88-415E-9C2D-D79F1224B62D','B275C127-4CD1-4DC8-86F7-C97D78C6CDFD',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('ED6C3250-9653-484D-AE90-8A1CC7A6ABE1','B275C287-5CD1-4DC8-86F7-C97D78C6CDFD',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('7A1EC17F-5E81-4A23-8917-B7F333945D19','B275C572-4CD1-4DC8-86F7-C78D78C6CDFD',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('04287397-8A61-44AA-9FDE-E7BB5CE9F9D9','B477AD9B-79E0-4BAB-B06D-6D1B47A01466',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('BA17FC75-FA6F-48FD-9E3D-F5E6803815FA','B6A9F553-3539-4203-AD94-65B6BDB41E24',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('465E863D-750A-4F49-9AE6-DDFAADF078DC','D4941BA1-91C2-41F2-97A6-B0411EC91D1B',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('C47660D2-1032-4D13-A591-ACCB24A02976','DB7B2E87-C279-4807-99F0-0B1728253807',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[is_begin],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('00E6729D-C067-4B44-B7D4-AB6E9CA2BB86','E70BDBD1-A62E-4196-8CDB-B0DC4184E541',0, '未处理','未处理',0,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
