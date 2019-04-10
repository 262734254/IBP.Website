truncate table [ibp_db].[dbo].[user_info];


INSERT INTO [ibp_db].[dbo].[user_info]
	([user_id],[role_id],[work_id],[cn_name],[en_name],[login_pwd],[login_name],[user_email],[entry_date],[positive_date],[leave_date],[department_id],[post_name],[team_name],[work_status],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'25D9BCAD-35A1-48E7-87DC-F10524389999',u.UserName,u.RealName,u.RealName,u.Password,u.UserName,null,null,null,null,'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',u.Question,u.Answer,'DD1C5D4E-46F9-4535-8462-661534DC2A1A',0, GETDATE(),'admin',null,null,0
from 
	Test_TXMS_20111129.dbo.lrs_Users u
GO

select * from [ibp_db].[dbo].[user_info]