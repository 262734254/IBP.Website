using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Common
{
    /// <summary>
    /// 排序枚举。
    /// </summary>
    public enum SortOrderEnum
    {
        /// <summary>
        /// 升序。
        /// </summary>
        ASC,

        /// <summary>
        /// 降序。
        /// </summary>
        DESC,
    }

    /// <summary>
    /// 登录状态枚举
    /// </summary>
    public enum LoginStatusEnum
    {
        /// <summary>
        /// 登录成功。
        /// </summary>
        Success,

        /// <summary>
        /// 用户名或密码错误。
        /// </summary>
        NameOrPwdError,

        /// <summary>
        /// 用户名或密码错误，需要验证校验码。
        /// </summary>
        NameOrPwdErrorAndShowValidCode,

        /// <summary>
        /// 用户被禁用。
        /// </summary>
        Disabled,

        /// <summary>
        /// 用户不存在。
        /// </summary>
        NotExists,
    }

    /// <summary>
    /// 部门级别。
    /// </summary>
    public enum DepartmentLevel
    {
        /// <summary>
        /// 上级部门。
        /// </summary>
        HigherLevel = 0,

        /// <summary>
        /// 同级部门。
        /// </summary>
        SameLevel = 1,

        /// <summary>
        /// 下级部门。
        /// </summary>
        LowerLevel = 2,
    }

    /// <summary>
    /// 获取工单数据的角色
    /// </summary>
    public enum GetWorkOrderRole
    {
        /// <summary>
        /// 获取所有工单。
        /// </summary>
        All,

        /// <summary>
        /// 获取分派给我的工单。
        /// </summary>
        Owner,

        /// <summary>
        /// 获取分派给我所属组的工单。
        /// </summary>
        OwnerGroup,
    }

    /// <summary>
    /// 工单处理状态枚举。
    /// </summary>
    public enum WorkOrderProcessStatus
    {
        /// <summary>
        /// 所有状态。
        /// </summary>
        All,

        /// <summary>
        /// 待处理。
        /// </summary>
        Waitting,

        /// <summary>
        /// 处理中。
        /// </summary>
        Processing,

        /// <summary>
        /// 已关闭。
        /// </summary>
        Closed,
    }

    /// <summary>
    /// 工单分配状态枚举。
    /// </summary>
    public enum WorkOrderAssignedStatus
    {
        /// <summary>
        /// 所有。
        /// </summary>
        All,

        /// <summary>
        /// 未分配。
        /// </summary>
        UnAssigned,

        /// <summary>
        /// 分配到组。
        /// </summary>
        AssignedToGroup,

        /// <summary>
        /// 分配到人。
        /// </summary>
        AssignedToUser,
    }

    /// <summary>
    /// 工单提醒类型枚举。
    /// </summary>
    public enum WorkOrderRemindType
    {
        /// <summary>
        /// 所有工单。
        /// </summary>
        All,

        /// <summary>
        /// 未设置。
        /// </summary>
        NoSet,

        /// <summary>
        /// 预约提醒。
        /// </summary>
        Advance,

        /// <summary>
        /// 过期提醒。
        /// </summary>
        Expired,
        /// <summary>
        /// 2小时预约到期工单
        /// </summary>
        TwoHourAppointmentExpired,
        /// <summary>
        /// 24小时预约到期工单
        /// </summary>
        TwentyFourExpired,
        /// <summary>
        /// 3预约到期工单
        /// </summary>
        ThreeDayExpired,
        /// <summary>
        /// 所有预约到期工单
        /// </summary>
        AllWillBeExpire,
        /// <summary>
        /// 2小时内即将过期工单
        /// </summary>
        TwohoursWillBeExpired,
        /// <summary>
        /// 24小时内即将过期工单
        /// </summary>
       TwentyWillBeExpired,
       /// <summary>
       /// 3天内即将过期工单
       /// </summary>
       ThreeDayWillBeExpired,
        /// <summary>
       /// 所有已经过期工单
        /// </summary>
       AllExpired,
        /// <summary>
       /// 所有即将过期工单
        /// </summary>
       ExpiredOrder,
    }
  
    /// <summary>
    /// 工单特殊处理状态枚举。
    /// </summary>
    public enum WorkOrderCustomStatus
    {
        /// <summary>
        /// 所有。
        /// </summary>
        All,

        /// <summary>
        /// 待审批。
        /// </summary>
        WaittingApproval,

        /// <summary>
        /// 待质检。
        /// </summary>
        WaittingQualityCheck,

        /// <summary>
        /// 已质检。
        /// </summary>
        QualityChecked,
    }

    /// <summary>
    /// 工单审批处理方式枚举。
    /// </summary>
    public enum WorkOrderApprovalAction
    {
        /// <summary>
        /// 转交他人处理。
        /// </summary>
        Assignment,

        /// <summary>
        /// 提交审批。
        /// </summary>
        SubmitApproval,

        /// <summary>
        /// 提交工单质检。
        /// </summary>
        QualityChecked,

        /// <summary>
        /// 关闭工单。
        /// </summary>
        CloseWorkOrder,
    }

    public enum OrderPayType
    {
                //<option value="">--请选择--</option>                
                //<option value="">无卡POS全额</option>
                //<option value="1">建行</option>
                //<option value="2">工行</option>
                //<option value="3">农行</option>
                //<option value="4">中行</option>
                //<option value="">无卡POS分期</option>
                //<option value="5">建行</option>
                //<option value="6">工行</option>
                //<option value="7">农行</option>
                //<option value="8">中行</option>
                //<option value="">货到刷卡全额</option>
                //<option value="9">建行</option>
                //<option value="10">工行</option>
                //<option value="11">农行</option>
                //<option value="12">中行</option>
                //<option value="">货到刷卡分期</option>
                //<option value="13">建行</option>
                //<option value="14">工行</option>
                //<option value="15">农行</option>
                //<option value="16">中行</option>
                //<option value="17">货到付现</option>

        /// <summary>
        /// 无卡POS全额(建行)
        /// </summary>
        NoCardPosFullPayment_CCB = 1,

        /// <summary>
        /// 无卡POS全额(工行)
        /// </summary>
        NoCardPosFullPayment_ICBC = 2,

        /// <summary>
        /// 无卡POS全额(农行)
        /// </summary>
        NoCardPosFullPayment_ABCHINA = 3,

        /// <summary>
        /// 无卡POS全额(中行)
        /// </summary>
        NoCardPosFullPayment_BOC = 4,

        /// <summary>
        /// 无卡POS分期(建行)
        /// </summary>
        NoCardPosInstallments_CCB = 5,

        /// <summary>
        /// 无卡POS分期(工行)
        /// </summary>
        NoCardPosInstallments_ICBC = 6,

        /// <summary>
        /// 无卡POS分期(农行)
        /// </summary>
        NoCardPosInstallments_ABCHINA = 7,

        /// <summary>
        /// 无卡POS分期(中行)
        /// </summary>
        NoCardPosInstallments_BOC = 8,


        /// <summary>
        /// 货到刷卡全额(建行)
        /// </summary>
        CardPayWhenReceive_CCB = 9,

        /// <summary>
        /// 货到刷卡全额(工行)
        /// </summary>
        CardPayWhenReceive_ICBC = 10,

        /// <summary>
        /// 货到刷卡全额(农行)
        /// </summary>
        CardPayWhenReceive_ABCHINA = 11,

        /// <summary>
        /// 货到刷卡全额(中行)
        /// </summary>
        CardPayWhenReceive_BOC = 12,


        /// <summary>
        /// 货到刷卡分期(建行)
        /// </summary>
        CardPayWhenReceiveInstallments_CCB = 13,

        /// <summary>
        /// 货到刷卡分期(工行)
        /// </summary>
        CardPayWhenReceiveInstallments_ICBC = 14,

        /// <summary>
        /// 货到刷卡分期(农行)
        /// </summary>
        CardPayWhenReceiveInstallments_ABCHINA = 15,

        /// <summary>
        /// 货到刷卡分期(中行)
        /// </summary>
        CardPayWhenReceiveInstallments_BOC = 16,


        /// <summary>
        /// 货到付现
        /// </summary>
        CashPayWhenReceive = 17,


    }

    /// <summary>
    /// 订单状态枚举。
    /// </summary>
    public enum SalesOrderStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All=0,
        /// <summary>
        /// 待跟进。
        /// </summary>
        WaitFollow = 1,

        /// <summary>
        /// 待扣款。
        /// </summary>
        WaitCharge = 2,	

        /// <summary>
        /// 待质检。
        /// </summary>
        WaitCheck = 3,	
        
        /// <summary>
        /// 待审批。
        /// </summary>
        WaitApproval = 4,

        /// <summary>
        /// 待开户。
        /// </summary>
        WaitOpening = 5,

        /// <summary>
        /// 待备货。
        /// </summary>
        WaitStocking = 6,

        /// <summary>
        /// 待发货。
        /// </summary>
        WaitDelivery = 7,

        /// <summary>
        /// 待签收。
        /// </summary>
        WaitSign = 8,

        /// <summary>
        /// 待回收。
        /// </summary>
        WaitRecover = 9,


        /// <summary>
        /// 成功。
        /// </summary>
        Successed = 10,


        /// <summary>
        /// 异常。
        /// </summary>
        Exception = 11,

	    /// <summary>
	    /// 待退款。
	    /// </summary>
        WaitRefund = 12,

        /// <summary>
        /// 待退货。
        /// </summary>
        WaitReturns = 13,

        /// <summary>
        /// 待销户。
        /// </summary>
        WaitCancelOpening = 14,    
    
        /// <summary>
        /// 撤消。
        /// </summary>
        Cancel = 15,

     

    }
}
