select 
	-- o.State as 订单状态,
	case 
		when o.State = 0 then '待跟进' 
		when o.State = 1 then '待扣款'
		when o.State = 2 then '待审批'
		when o.State = 3 then '待开户'
		when o.State = 4 then '待备货'
		when o.State = 5 then '待发货'
		when o.State = 6 then '待签收'
		when o.State = 7 then '待回收'
		when o.State = 8 then '成功'
		when o.State = 9 then '异常'
		when o.State = 10 then '待撤消'
		when o.State = 11 then '已撤消'
		when o.State = 12 then '销售结束'
		when o.State = 13 then 'CallBack'
		when o.State = 14 then '已添加联系记录'
		when o.State = 13 then 'CallBack'
	end as 订单状态,
	o.CID as 客户编号,
	o.ID as 订单唯一标识,
	o.SelNum as 套餐号码,
	o.AddDate as 订单时间,
	o.KKDate as 扣款时间,
	c.YXQ as 有效期,
	cast(convert(varchar(100), '20' + RIGHT(REPLACE(c.YXQ, ' ',''),2) + '-' + LEFT(REPLACE(c.YXQ, ' ',''),2) + '-01', 23) as datetime) as 有效期时间,
	case when
		dateadd(month, 36, o.KKDate) > cast(convert(varchar(100), '20' + RIGHT(REPLACE(c.YXQ, ' ',''),2) + '-' + LEFT(REPLACE(c.YXQ, ' ',''),2) + '-01', 23) as datetime)
		then '超出' else '未超出'
	end as 结果
from 
	Order_BaseInfo as o
left join
	C_CardInfo as c on o.CardID = c.ID
where
	o.KKDate is not null
	and o.KKDate <> '1900-01-01 00:00:00'
	and c.YXQ is not null
	and c.YXQ <> '0000'
	and len(c.YXQ)=4
	and o.State <> 3
	and o.State <> 7
	and o.State <> 9
	and o.State <> 11
	-- and dateadd(month, 36, o.KKDate) < cast(convert(varchar(100), '20' + RIGHT(REPLACE(c.YXQ, ' ',''),2) + '-' + LEFT(REPLACE(c.YXQ, ' ',''),2) + '-01', 23) as datetime)
