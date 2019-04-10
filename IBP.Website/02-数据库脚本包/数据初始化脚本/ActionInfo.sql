truncate table action_info;
INSERT INTO action_info
	([action_id],[node_id],[sort_order],[parent_node],[action_name],[action_type],[action_group],[display_name],[controller_name],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	-- 呼叫中心
	(NEWID(),1,1,0,'CallCenter_Index',0,'CallCenter_Index','呼叫中心','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 呼叫中心 子级菜单
	(NEWID(),101,101,1,'MySales',0,'MySales','我的营销','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),102,102,1,'CustomerMgr_Index',0,'CustomerMgr_Index','客户管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 呼叫中心->客户管理 子级菜单
	(NEWID(),10201,10201,102,'Distribution',0,'Distribution','客户分配管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),10202,10202,102,'Level',0,'Level','客户级别管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),10203,10203,102,'CallBackList',0,'CallBackList','CallBack任务列表','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),103,103,1,'NumberQuery',0,'NumberQuery','号码查询','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),104,104,1,'OrderQuery',0,'OrderQuery','订单查询','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 订单中心	
	(NEWID(),2,2,0,'OrderCenter_Index',0,'OrderCenter_Index','订单中心','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 订单中心 子级菜单	
	(NEWID(),201,201,2,'MyExceptionOrder',0,'MyExceptionOrder','异常单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),202,202,2,'AllExceptionOrder',0,'AllExceptionOrder','所有异常单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),203,203,2,'WaitChargeOrder',0,'WaitChargeOrder','待扣款单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),204,204,2,'WaitApprovalOrder',0,'WaitApprovalOrder','待审批单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),205,205,2,'WaitOpeningOrder',0,'WaitOpeningOrder','待开户单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),206,206,2,'WaitStockingOrder',0,'WaitStockingOrder','待备货单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),207,207,2,'WaitDeliveryOrder',0,'WaitDeliveryOrder','待发货单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),208,208,2,'WaitSignOrder',0,'WaitSignOrder','待签收单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),209,209,2,'WaitRecoverOrder',0,'WaitRecoverOrder','待回收单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),210,210,2,'RevokedOrder',0,'RevokedOrder','撤消单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),211,211,2,'WaitRefundOrder',0,'WaitRefundOrder','待撤销单（退款）','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),212,212,2,'WaitReturnsOrder',0,'WaitReturnsOrder','待撤销单（退货）','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),213,213,2,'OrderManager',0,'OrderManager','订单管理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 业务中心	
	(NEWID(),3,3,0,'BusinessCenter_Index',0,'BusinessCenter_Index','业务中心','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 业务中心 子级菜单	
	(NEWID(),301,301,3,'NumberManager',0,'NumberManager','号码管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),302,302,3,'ProjectManager',0,'ProjectManager','项目管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),303,303,3,'BroadcastManager',0,'BroadcastManager','广播管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 工单中心	
	(NEWID(),4,4,0,'WorkOrderCenter_Index',0,'WorkOrderCenter_Index','工单中心','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 工单中心 子级菜单	
	(NEWID(),401,401,4,'PendingWorkOrder',0,'PendingWorkOrder','待处理工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),402,402,4,'ProcessingWorkOrder',0,'ProcessingWorkOrder','处理中工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),403,403,4,'WorkOrderManager',0,'WorkOrderManager','工单管理','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 报表中心	
	(NEWID(),5,5,0,'ReportCenter_Index',0,'ReportCenter_Index','报表中心','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 报表中心 子级菜单	
	(NEWID(),501,501,5,'OrderReport',0,'OrderReport','订单管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),502,502,5,'BusinessAnalysisReport',0,'BusinessAnalysisReport','业务分析报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),503,503,5,'ContactReport',0,'ContactReport','联系管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),504,504,5,'CustomerReportMgr_Index',0,'CustomerReportMgr_Index','客户管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 报表中心->客户管理报表 子级菜单	
	(NEWID(),50401,50401,504,'CustomerConversionRate',0,'CustomerConversionRate','客户转化率报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),505,505,5,'WorkOrderReport',0,'WorkOrderReport','工单管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),506,506,5,'ChargeOrderReport',0,'ChargeOrderReport','扣款报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),507,507,5,'DeliveryQuery',0,'DeliveryQuery','物流综合查询','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),508,508,5,'OpeningOrderReport',0,'OpeningOrderReport','开户报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),509,509,5,'RecoverOrderReport',0,'RecoverOrderReport','回收报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 研发中心	
	(NEWID(),6,6,0,'CentreCenter_Index',0,'CentreCenter_Index','研发中心','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 研发中心 子级菜单
	(NEWID(),601,601,6,'CentreProduct_Index',0,'CentreProduct_Index','产品视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 研发中心->产品视图 子级菜单
	(NEWID(),60101,60101,601,'ProductList',0,'CentreProduct_Index','产品列表','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60102,60102,601,'RequirementsMgr',0,'RequirementsMgr','需求管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60103,60103,601,'PlanMgr',0,'PlanMgr','计划管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60104,60104,601,'ReleaseMgr',0,'ReleaseMgr','发布管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60105,60105,601,'Roadmap',0,'Roadmap','线路图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60106,60106,601,'ProductDoc',0,'ProductDoc','产品文档','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60107,60107,601,'ProductModule',0,'ProductModule','模块管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),602,602,6,'CentreProject_Index',0,'CentreProject_Index','项目视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 研发中心->项目视图 子级菜单
	(NEWID(),60201,60201,602,'ProjectRequirements',0,'ProjectRequirements','项目需求','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60202,60202,602,'ProjectTaskList',0,'ProjectTaskList','计划任务','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60203,60203,602,'Burndown',0,'Burndown','燃尽图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60204,60204,602,'ProjectDoc',0,'ProjectDoc','项目文档','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60205,60205,602,'BugMgr',0,'BugMgr','BUG管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60206,60206,602,'ProjectRelease',0,'ProjectRelease','发布管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60207,60207,602,'ProjectTeam',0,'ProjectTeam','团队管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),603,603,6,'CentreTest_Index',0,'CentreTest_Index','测试视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 研发中心->测试视图 子级菜单
	(NEWID(),60301,60301,603,'BugMgr',0,'BugMgr','缺陷管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60302,60302,603,'TestCaseMgr',0,'TestCaseMgr','用例管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60303,60303,603,'TestListMgr',0,'TestListMgr','测试任务','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),604,604,6,'CentreDocument_Index',0,'CentreDocument_Index','文档视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 研发中心->文档视图 子级菜单
	(NEWID(),60401,60401,604,'DocLibMgr',0,'DocLibMgr','文档库管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60402,60402,604,'DocList',0,'DocList','文档列表','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60403,60403,604,'DocModule',0,'DocModule','模块管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 系统管理	
	(NEWID(),7,7,0,'System_Index',0,'System_Index','系统管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 系统管理	子级菜单
	(NEWID(),701,701,7,'UserList',0,'UserList','用户管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('90B724BA-8021-4D89-92BE-410D1134B80F',702,702,7,'DepList',0,'DepList','部门管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 系统管理->部门管理 包含操作列表
--	([action_id],[node_id],[sort_order],[parent_node],[action_name],[action_type],[action_group],[display_name],[_name],[created_on],[created_by],[modified_on],[modified_by],[status_code])
	(NEWID(),70201,70201,702,'DepUserList',1,'DepList','获取部门用户操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70202,70202,702,'DoAddDepartmentUser',1,'DepList','添加部门成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70203,70203,702,'DoRemoveDepartmentUser',1,'DepList','删除部门成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70204,70204,702,'NewDepartment',1,'DepList','新建部门视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70205,70205,702,'DoNewDepartment',1,'DepList','新建部门操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70206,70206,702,'EditDepartment',1,'DepList','编辑部门视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70207,70207,702,'DoUpdateDepartment',1,'DepList','更新部门操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70208,70208,702,'DoDeleteDepartment',1,'DepList','删除部门操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	
	(NEWID(),703,703,7,'Premission',0,'Premission','权限设置','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 系统管理->权限设置 包含操作列表
	(NEWID(),70301,70301,703,'RoleUserList',1,'Premission','获取角色用户列表','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70302,70302,703,'AddRoleUser',1,'Premission','新增角色用户视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70303,70303,703,'NewRole',1,'Premission','新增角色视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70304,70304,703,'EditRole',1,'Premission','编辑角色视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70305,70305,703,'DoNewRole',1,'Premission','新增角色操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70306,70306,703,'DoUpdateRole',1,'Premission','更新角色操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70307,70307,703,'DoDeleteRole',1,'Premission','删除角色操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70308,70308,703,'DoAddRoleUserList',1,'Premission','添加角色成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70309,70309,703,'DoRemoveRoleUser',1,'Premission','移除角色成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),


	
	(NEWID(),704,704,7,'CustomInfo',0,'CustomInfo','枚举信息管理','System',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),705,705,7,'BankCardInfo',0,'BankCardInfo','银行卡信息管理','System',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),706,706,7,'SysLogs',0,'SysLogs','系统日志','System',GETDATE(),'admin',GETDATE(),'admin',0),
	
	(NEWID(),8,8,0,'Index',1,'Home_Index','首页','Home',GETDATE(),'admin',GETDATE(),'admin',0)

GO


--select * from action_info where action_group = 'DepList'