truncate table [ibp_db].[dbo].[department_info];

INSERT INTO [ibp_db].[dbo].[department_info]
	([sort_order],[department_id],[parent_id],[department_name],[description],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(0,'1D8352A7-1EA6-45A1-86FA-23171B962A3E',null,'�ܾ���',null,GETDATE(),'admin',null,null,0),
	(1,'2F13F5DD-EBD3-4CE2-8E12-4261F883AF50',null,'ս�Է�չ����',null,GETDATE(),'admin',null,null,0),
	(2,'A7611285-8243-48E7-AD90-F083F57B152E',null,'Ӫ������',null,GETDATE(),'admin',null,null,0),

	(201,'7A96841D-5503-4DF3-B1DB-22F8448BFF50','A7611285-8243-48E7-AD90-F083F57B152E','�ʿز�',null,GETDATE(),'admin',null,null,0),
	(202,'AFBD40DA-3934-4C73-89DA-395B277B708F','A7611285-8243-48E7-AD90-F083F57B152E','������',null,GETDATE(),'admin',null,null,0),
	(203,'C30FC6C1-0DD9-4D81-A8CC-BDD1295CD652','A7611285-8243-48E7-AD90-F083F57B152E','�ͻ���������',null,GETDATE(),'admin',null,null,0),


	(3,'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',null,'������',null,GETDATE(),'admin',null,null,0),
	(4,'12F679F9-441B-4708-8FB1-17E29AA601C3',null,'������Դ��',null,GETDATE(),'admin',null,null,0),
	(5,'DB3404A3-0019-49F8-ADC0-23DCD55C4251',null,'����',null,GETDATE(),'admin',null,null,0)
	
	
GO

select * from [ibp_db].[dbo].[department_info] order by sort_order;

