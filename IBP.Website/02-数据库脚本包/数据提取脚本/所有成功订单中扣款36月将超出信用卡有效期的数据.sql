select 
	-- o.State as ����״̬,
	case 
		when o.State = 0 then '������' 
		when o.State = 1 then '���ۿ�'
		when o.State = 2 then '������'
		when o.State = 3 then '������'
		when o.State = 4 then '������'
		when o.State = 5 then '������'
		when o.State = 6 then '��ǩ��'
		when o.State = 7 then '������'
		when o.State = 8 then '�ɹ�'
		when o.State = 9 then '�쳣'
		when o.State = 10 then '������'
		when o.State = 11 then '�ѳ���'
		when o.State = 12 then '���۽���'
		when o.State = 13 then 'CallBack'
		when o.State = 14 then '�������ϵ��¼'
		when o.State = 13 then 'CallBack'
	end as ����״̬,
	o.CID as �ͻ����,
	o.ID as ����Ψһ��ʶ,
	o.SelNum as �ײͺ���,
	o.AddDate as ����ʱ��,
	o.KKDate as �ۿ�ʱ��,
	c.YXQ as ��Ч��,
	cast(convert(varchar(100), '20' + RIGHT(REPLACE(c.YXQ, ' ',''),2) + '-' + LEFT(REPLACE(c.YXQ, ' ',''),2) + '-01', 23) as datetime) as ��Ч��ʱ��,
	case when
		dateadd(month, 36, o.KKDate) > cast(convert(varchar(100), '20' + RIGHT(REPLACE(c.YXQ, ' ',''),2) + '-' + LEFT(REPLACE(c.YXQ, ' ',''),2) + '-01', 23) as datetime)
		then '����' else 'δ����'
	end as ���
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
