select * from BD_YWInfo
select * from BD_NumInfo

select TSZH,REPLACE(ZDSF,'省','') as province, replace(ZDCS,'市','') as city, replace(ZDQX,'区','') as county,ZDDZ,* from Order_BaseInfo where ZDDZ <> '' and TSZH <> '' and TSZH <> '0' and CID in (select ID from C_Info)

select county from (
select REPLACE(PSSF,'省','') as province, replace(PSCS,'市','') as city, replace(PSQX,'区','') as county from Order_BaseInfo where PSSF <> '' and PSDZ is not null and PSDZ <> '' and PSDZ <> '广东省 选择市 选择区/县 ' and CID in (select ID from C_Info)
) aa group by county


select * from C_CardInfo where CardNum <> '' and CID in (select ID from C_Info)

select u.UserName,c.* from C_LXJL c left join lrs_Users u on c.UserID = u.UserID where AddDate is null


select * from C_Info where ID = 'C11000000022'
select * from C_BZXX where CID in (select ID from C_Info)
select * from C_CardInfo where CardNum <> '' and CID in (select ID from C_Info)

select CID,TSZH,TSHM,ZJLX,ZJNum,ZDDZ,ZDYB,* from Order_BaseInfo where TSZH <> '' and TSZH <> '0' and TSZH not in (select CardNum from C_CardInfo)


select u.UserName, c.* from C_BZXX c left join lrs_Users u on c.UserID = u.UserID where CID in (select ID from C_Info)

select CardSelect, COUNT(1) from Order_BaseInfo group by CardSelect
select 
	o.cid,o.JZXM,o.ZJLX,o.ZJNum, c.CName
from 
	Order_BaseInfo o 
left join
	C_Info c
on c.id = o.cid
where 
	o.CardSelect = 1 and o.JZXM is not null and o.JZXM <> ''




select id, COUNT(1) from C_Info group by id
select * from C_CardInfo where ZJNum = '' or ZJNum = '0' CardNum <> ''
select ZJLX, ZJNum from C_CardInfo where ZJNum <> '0' and ZJNum <> ''

select * from C_Info where ID = 'C11000000447'
select u.UserName,c.* from C_LXJL c left join lrs_Users u on c.UserID = u.UserID

select CLevel,COUNT(1) from C_Info group by CLevel;

select * from C_Info where Tel is not null and Tel <> ''

select ZJLX, ZJNum, * from C_CardInfo where ZJNum <> '0'
select ZJLX, ZJNum, * from Order_BaseInfo where ZJNum <> '0' and ZJNum <> ''

select * from lrs_Users 
select u.UserName, c.* from C_BZXX c left join lrs_Users u on c.UserID = u.UserID

