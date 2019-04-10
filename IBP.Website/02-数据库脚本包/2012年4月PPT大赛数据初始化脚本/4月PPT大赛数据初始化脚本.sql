--select NEWID();
truncate table [ibp_db].[dbo].[vote_project_info];

INSERT INTO [ibp_db].[dbo].[vote_project_info]
           ([vote_project_id],[project_name],[description],[begin_time],[end_time],[can_muilt_vote],[can_anonymous],can_modify_vote,[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
     VALUES
           ('A0B4B4C5-B196-48E2-B00D-7E50921E0675','2012年4月盈兴PPT大赛','','2012-04-20','2012-04-30',1,1,0,0,GETDATE(),null,null,null,0);
GO

select * from vote_project_info;

truncate table [vote_project_item_info];

INSERT INTO [ibp_db].[dbo].[vote_project_item_info]
           (sort_order, [vote_item_id],[vote_project_id],[item_title],[description],[attachment_path],[vote_total],[vote_score],[candidater],[candidater_name],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
     VALUES
           (1, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0001',null,'/uploads/attachment-1.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),           
           (2, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0002',null,'/uploads/attachment-2.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (3, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0003',null,'/uploads/attachment-3.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (4, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0004',null,'/uploads/attachment-4.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (5, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0005',null,'/uploads/attachment-5.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (6, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0006',null,'/uploads/attachment-6.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (7, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0007',null,'/uploads/attachment-7.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (8, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0008',null,'/uploads/attachment-8.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (9, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0009',null,'/uploads/attachment-9.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0),
           (10, NEWID(),'A0B4B4C5-B196-48E2-B00D-7E50921E0675','参赛作品-0010',null,'/uploads/attachment-10.ppt',0,0,'1212','周强',0,GETDATE(),null,null,null,0);
           
select * from [vote_project_item_info] where vote_project_id = 'A0B4B4C5-B196-48E2-B00D-7E50921E0675';
GO




select t.candidater_name, t.item_title,a.avgg,a.summ from (
select 
	vote_item_id,
	SUM(cast(score as int)) as summ,
	AVG(cast(score as int)) as avgg 
from 
	rel_user_voteitem 
group by 
	vote_item_id
) a 
left join 
	vote_project_item_info t
on 
	a.vote_item_id = t.vote_item_id
order by 
	t.sort_order
	
	
	
-- select * from rel_user_voteitem;

-- truncate table rel_user_voteitem;