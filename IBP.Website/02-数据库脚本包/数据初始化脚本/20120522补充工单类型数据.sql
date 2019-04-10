
-- select * from workorder_type_info where type_name = '售后处理';
update workorder_type_info set type_name = '售后工单' where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016';
update workorder_type_info set type_name = '节日问候' where workorder_type_id = '29807854-4C2C-4D4C-A3C7-9585812C7629';
update workorder_type_info set type_name = '营销工单' where workorder_type_id = 'B6A9F553-3539-4203-AD94-65B6BDB41E24';

update workorder_type_info set parent_id = '82A5F0F9-36CA-4F94-8325-DAAC77600541' 
where type_name in 
(
'拆机维挽',
'停机维挽',
'生日关怀',
'托收激活',
'节日问候',
'WelcomeCall'
)

delete from workorder_type_info where workorder_type_id in 
(
'7D0D3814-133E-49C6-8EED-02963D1C4F35',
'D8899AA4-128A-4E34-B3FE-59F1AAC2FB52',
'2A875D48-5E95-4794-9903-405493BD8F66',
'F8ABA708-BBCD-4EE4-9C0C-5290A45966D4',
'5494A514-8A04-477A-A02D-D756E28F04F6',
'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8');


INSERT INTO [workorder_type_info]
           ([workorder_type_id],[parent_id],[type_name],[description],[workorder_type_status],[sort_order],[created_on],[created_by],[status_code])
VALUES
		   ('EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8',null, '内部工单','内部工单',0,0,getdate(),'Admin',0),
		   ('7D0D3814-133E-49C6-8EED-02963D1C4F35','2403E962-1A50-4925-9F78-D43623598016','终端售后','终端售后',0,0,getdate(),'Admin',0),
		   ('D8899AA4-128A-4E34-B3FE-59F1AAC2FB52','2403E962-1A50-4925-9F78-D43623598016','电信类售后','电信类售后',0,0,getdate(),'Admin',0),
		   ('2A875D48-5E95-4794-9903-405493BD8F66','2403E962-1A50-4925-9F78-D43623598016','银行类售后','银行类售后',0,0,getdate(),'Admin',0),
		   ('F8ABA708-BBCD-4EE4-9C0C-5290A45966D4','2403E962-1A50-4925-9F78-D43623598016','物流类售后','物流类售后',0,0,getdate(),'Admin',0),
		   ('5494A514-8A04-477A-A02D-D756E28F04F6','2403E962-1A50-4925-9F78-D43623598016','综合(其他)售后','综合(其他)售后',0,0,getdate(),'Admin',0);

update workorder_type_info set type_name = '促销申请', parent_id = 'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8' where workorder_type_id = '7E757BCC-8813-4AA9-AF5A-537E144DA51A';

delete from [workorder_status_info] where [workorder_type_id] = 'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8';
delete from [workorder_status_info] where [workorder_type_id] = '7D0D3814-133E-49C6-8EED-02963D1C4F35';
delete from [workorder_status_info] where [workorder_type_id] = 'D8899AA4-128A-4E34-B3FE-59F1AAC2FB52';
delete from [workorder_status_info] where [workorder_type_id] = '2A875D48-5E95-4794-9903-405493BD8F66';
delete from [workorder_status_info] where [workorder_type_id] = 'F8ABA708-BBCD-4EE4-9C0C-5290A45966D4';
delete from [workorder_status_info] where [workorder_type_id] = '5494A514-8A04-477A-A02D-D756E28F04F6';

delete from [workorder_result_info] where [workorder_type_id] = 'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8';
delete from [workorder_result_info] where [workorder_type_id] = '7D0D3814-133E-49C6-8EED-02963D1C4F35';
delete from [workorder_result_info] where [workorder_type_id] = 'D8899AA4-128A-4E34-B3FE-59F1AAC2FB52';
delete from [workorder_result_info] where [workorder_type_id] = '2A875D48-5E95-4794-9903-405493BD8F66';
delete from [workorder_result_info] where [workorder_type_id] = 'F8ABA708-BBCD-4EE4-9C0C-5290A45966D4';
delete from [workorder_result_info] where [workorder_type_id] = '5494A514-8A04-477A-A02D-D756E28F04F6';

-- 处理状态
INSERT INTO [workorder_status_info] ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select newid(),'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code] from workorder_status_info where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016' order by sort_order;

INSERT INTO [workorder_status_info] ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select newid(),'7D0D3814-133E-49C6-8EED-02963D1C4F35',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code] from workorder_status_info where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016' order by sort_order;
	
INSERT INTO [workorder_status_info] ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select newid(),'D8899AA4-128A-4E34-B3FE-59F1AAC2FB52',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code] from workorder_status_info where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016' order by sort_order;
	
INSERT INTO [workorder_status_info] ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select newid(),'2A875D48-5E95-4794-9903-405493BD8F66',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code] from workorder_status_info where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016' order by sort_order;
	
INSERT INTO [workorder_status_info] ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select newid(),'F8ABA708-BBCD-4EE4-9C0C-5290A45966D4',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code] from workorder_status_info where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016' order by sort_order;
	
INSERT INTO [workorder_status_info] ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select newid(),'5494A514-8A04-477A-A02D-D756E28F04F6',[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code] from workorder_status_info where workorder_type_id = '2403E962-1A50-4925-9F78-D43623598016' order by sort_order;
	

-- 处理结果
INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select NEWID(),'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0 from [workorder_result_info] where workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629' order by sort_order;	

INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select NEWID(),'7D0D3814-133E-49C6-8EED-02963D1C4F35',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0 from [workorder_result_info] where workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629' order by sort_order;	

INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select NEWID(),'D8899AA4-128A-4E34-B3FE-59F1AAC2FB52',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0 from [workorder_result_info] where workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629' order by sort_order;	

INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select NEWID(),'2A875D48-5E95-4794-9903-405493BD8F66',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0 from [workorder_result_info] where workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629' order by sort_order;	

INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select NEWID(),'F8ABA708-BBCD-4EE4-9C0C-5290A45966D4',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0 from [workorder_result_info] where workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629' order by sort_order;	

INSERT INTO [workorder_result_info] ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select NEWID(),'5494A514-8A04-477A-A02D-D756E28F04F6',[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],0 from [workorder_result_info] where workorder_type_id = '29807854-4c2c-4d4c-a3c7-9585812c7629' order by sort_order;	
 
 
select * from workorder_result_info where workorder_type_id in
(
'EB7F5E19-42D1-4CC0-AAEE-DC393F5706A8',
'7D0D3814-133E-49C6-8EED-02963D1C4F35',
'D8899AA4-128A-4E34-B3FE-59F1AAC2FB52',
'2A875D48-5E95-4794-9903-405493BD8F66',
'F8ABA708-BBCD-4EE4-9C0C-5290A45966D4',
'5494A514-8A04-477A-A02D-D756E28F04F6'
)

update  workorder_status_info set custom_status = status_name; -- where (status_name = '待审批' or status_name = '待质检')
 
update workorder_result_info set is_begin = 0 where result_name = '未处理';

-- select * from workorder_result_info where result_name = '其他';
update workorder_result_info set status = 1 where result_name = '其他';