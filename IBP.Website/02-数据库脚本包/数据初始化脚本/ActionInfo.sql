truncate table action_info;
INSERT INTO action_info
	([action_id],[node_id],[sort_order],[parent_node],[action_name],[action_type],[action_group],[display_name],[controller_name],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	-- ��������
	(NEWID(),1,1,0,'CallCenter_Index',0,'CallCenter_Index','��������','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- �������� �Ӽ��˵�
	(NEWID(),101,101,1,'MySales',0,'MySales','�ҵ�Ӫ��','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),102,102,1,'CustomerMgr_Index',0,'CustomerMgr_Index','�ͻ�����','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- ��������->�ͻ����� �Ӽ��˵�
	(NEWID(),10201,10201,102,'Distribution',0,'Distribution','�ͻ��������','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),10202,10202,102,'Level',0,'Level','�ͻ��������','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),10203,10203,102,'CallBackList',0,'CallBackList','CallBack�����б�','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),103,103,1,'NumberQuery',0,'NumberQuery','�����ѯ','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),104,104,1,'OrderQuery',0,'OrderQuery','������ѯ','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- ��������	
	(NEWID(),2,2,0,'OrderCenter_Index',0,'OrderCenter_Index','��������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �������� �Ӽ��˵�	
	(NEWID(),201,201,2,'MyExceptionOrder',0,'MyExceptionOrder','�쳣��','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),202,202,2,'AllExceptionOrder',0,'AllExceptionOrder','�����쳣��','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),203,203,2,'WaitChargeOrder',0,'WaitChargeOrder','���ۿ','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),204,204,2,'WaitApprovalOrder',0,'WaitApprovalOrder','��������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),205,205,2,'WaitOpeningOrder',0,'WaitOpeningOrder','��������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),206,206,2,'WaitStockingOrder',0,'WaitStockingOrder','��������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),207,207,2,'WaitDeliveryOrder',0,'WaitDeliveryOrder','��������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),208,208,2,'WaitSignOrder',0,'WaitSignOrder','��ǩ�յ�','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),209,209,2,'WaitRecoverOrder',0,'WaitRecoverOrder','�����յ�','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),210,210,2,'RevokedOrder',0,'RevokedOrder','������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),211,211,2,'WaitRefundOrder',0,'WaitRefundOrder','�����������˿','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),212,212,2,'WaitReturnsOrder',0,'WaitReturnsOrder','�����������˻���','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),213,213,2,'OrderManager',0,'OrderManager','��������','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- ҵ������	
	(NEWID(),3,3,0,'BusinessCenter_Index',0,'BusinessCenter_Index','ҵ������','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- ҵ������ �Ӽ��˵�	
	(NEWID(),301,301,3,'NumberManager',0,'NumberManager','�������','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),302,302,3,'ProjectManager',0,'ProjectManager','��Ŀ����','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),303,303,3,'BroadcastManager',0,'BroadcastManager','�㲥����','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- ��������	
	(NEWID(),4,4,0,'WorkOrderCenter_Index',0,'WorkOrderCenter_Index','��������','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �������� �Ӽ��˵�	
	(NEWID(),401,401,4,'PendingWorkOrder',0,'PendingWorkOrder','��������','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),402,402,4,'ProcessingWorkOrder',0,'ProcessingWorkOrder','�����й���','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),403,403,4,'WorkOrderManager',0,'WorkOrderManager','��������','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- ��������	
	(NEWID(),5,5,0,'ReportCenter_Index',0,'ReportCenter_Index','��������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �������� �Ӽ��˵�	
	(NEWID(),501,501,5,'OrderReport',0,'OrderReport','����������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),502,502,5,'BusinessAnalysisReport',0,'BusinessAnalysisReport','ҵ���������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),503,503,5,'ContactReport',0,'ContactReport','��ϵ������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),504,504,5,'CustomerReportMgr_Index',0,'CustomerReportMgr_Index','�ͻ�������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- ��������->�ͻ������� �Ӽ��˵�	
	(NEWID(),50401,50401,504,'CustomerConversionRate',0,'CustomerConversionRate','�ͻ�ת���ʱ���','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),505,505,5,'WorkOrderReport',0,'WorkOrderReport','����������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),506,506,5,'ChargeOrderReport',0,'ChargeOrderReport','�ۿ��','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),507,507,5,'DeliveryQuery',0,'DeliveryQuery','�����ۺϲ�ѯ','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),508,508,5,'OpeningOrderReport',0,'OpeningOrderReport','��������','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),509,509,5,'RecoverOrderReport',0,'RecoverOrderReport','���ձ���','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- �з�����	
	(NEWID(),6,6,0,'CentreCenter_Index',0,'CentreCenter_Index','�з�����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �з����� �Ӽ��˵�
	(NEWID(),601,601,6,'CentreProduct_Index',0,'CentreProduct_Index','��Ʒ��ͼ','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �з�����->��Ʒ��ͼ �Ӽ��˵�
	(NEWID(),60101,60101,601,'ProductList',0,'CentreProduct_Index','��Ʒ�б�','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60102,60102,601,'RequirementsMgr',0,'RequirementsMgr','�������','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60103,60103,601,'PlanMgr',0,'PlanMgr','�ƻ�����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60104,60104,601,'ReleaseMgr',0,'ReleaseMgr','��������','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60105,60105,601,'Roadmap',0,'Roadmap','��·ͼ','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60106,60106,601,'ProductDoc',0,'ProductDoc','��Ʒ�ĵ�','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60107,60107,601,'ProductModule',0,'ProductModule','ģ�����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),602,602,6,'CentreProject_Index',0,'CentreProject_Index','��Ŀ��ͼ','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �з�����->��Ŀ��ͼ �Ӽ��˵�
	(NEWID(),60201,60201,602,'ProjectRequirements',0,'ProjectRequirements','��Ŀ����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60202,60202,602,'ProjectTaskList',0,'ProjectTaskList','�ƻ�����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60203,60203,602,'Burndown',0,'Burndown','ȼ��ͼ','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60204,60204,602,'ProjectDoc',0,'ProjectDoc','��Ŀ�ĵ�','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60205,60205,602,'BugMgr',0,'BugMgr','BUG����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60206,60206,602,'ProjectRelease',0,'ProjectRelease','��������','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60207,60207,602,'ProjectTeam',0,'ProjectTeam','�Ŷӹ���','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),603,603,6,'CentreTest_Index',0,'CentreTest_Index','������ͼ','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �з�����->������ͼ �Ӽ��˵�
	(NEWID(),60301,60301,603,'BugMgr',0,'BugMgr','ȱ�ݹ���','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60302,60302,603,'TestCaseMgr',0,'TestCaseMgr','��������','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60303,60303,603,'TestListMgr',0,'TestListMgr','��������','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	(NEWID(),604,604,6,'CentreDocument_Index',0,'CentreDocument_Index','�ĵ���ͼ','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- �з�����->�ĵ���ͼ �Ӽ��˵�
	(NEWID(),60401,60401,604,'DocLibMgr',0,'DocLibMgr','�ĵ������','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60402,60402,604,'DocList',0,'DocList','�ĵ��б�','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),60403,60403,604,'DocModule',0,'DocModule','ģ�����','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- ϵͳ����	
	(NEWID(),7,7,0,'System_Index',0,'System_Index','ϵͳ����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),

	-- ϵͳ����	�Ӽ��˵�
	(NEWID(),701,701,7,'UserList',0,'UserList','�û�����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('90B724BA-8021-4D89-92BE-410D1134B80F',702,702,7,'DepList',0,'DepList','���Ź���','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- ϵͳ����->���Ź��� ���������б�
--	([action_id],[node_id],[sort_order],[parent_node],[action_name],[action_type],[action_group],[display_name],[_name],[created_on],[created_by],[modified_on],[modified_by],[status_code])
	(NEWID(),70201,70201,702,'DepUserList',1,'DepList','��ȡ�����û�����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70202,70202,702,'DoAddDepartmentUser',1,'DepList','��Ӳ��ų�Ա����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70203,70203,702,'DoRemoveDepartmentUser',1,'DepList','ɾ�����ų�Ա����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70204,70204,702,'NewDepartment',1,'DepList','�½�������ͼ','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70205,70205,702,'DoNewDepartment',1,'DepList','�½����Ų���','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70206,70206,702,'EditDepartment',1,'DepList','�༭������ͼ','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70207,70207,702,'DoUpdateDepartment',1,'DepList','���²��Ų���','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70208,70208,702,'DoDeleteDepartment',1,'DepList','ɾ�����Ų���','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	
	(NEWID(),703,703,7,'Premission',0,'Premission','Ȩ������','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- ϵͳ����->Ȩ������ ���������б�
	(NEWID(),70301,70301,703,'RoleUserList',1,'Premission','��ȡ��ɫ�û��б�','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70302,70302,703,'AddRoleUser',1,'Premission','������ɫ�û���ͼ','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70303,70303,703,'NewRole',1,'Premission','������ɫ��ͼ','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70304,70304,703,'EditRole',1,'Premission','�༭��ɫ��ͼ','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70305,70305,703,'DoNewRole',1,'Premission','������ɫ����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70306,70306,703,'DoUpdateRole',1,'Premission','���½�ɫ����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70307,70307,703,'DoDeleteRole',1,'Premission','ɾ����ɫ����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70308,70308,703,'DoAddRoleUserList',1,'Premission','��ӽ�ɫ��Ա����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),70309,70309,703,'DoRemoveRoleUser',1,'Premission','�Ƴ���ɫ��Ա����','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),


	
	(NEWID(),704,704,7,'CustomInfo',0,'CustomInfo','ö����Ϣ����','System',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),705,705,7,'BankCardInfo',0,'BankCardInfo','���п���Ϣ����','System',GETDATE(),'admin',GETDATE(),'admin',0),
	(NEWID(),706,706,7,'SysLogs',0,'SysLogs','ϵͳ��־','System',GETDATE(),'admin',GETDATE(),'admin',0),
	
	(NEWID(),8,8,0,'Index',1,'Home_Index','��ҳ','Home',GETDATE(),'admin',GETDATE(),'admin',0)

GO


--select * from action_info where action_group = 'DepList'