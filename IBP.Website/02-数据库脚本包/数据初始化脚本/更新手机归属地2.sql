select * from phone_location_info where china_id is null group by city

select top 1 * from phone_location_info where phone_code = '1392651'

update phone_location_info set city = '�����ɹ������������' where city = '����';
update phone_location_info set city = '�����鲼', province='���ɹ�' where city = '���������鲼' and province = '���������鲼';
select * from phone_location_info where city = '����';
select * from china_info where id = 493;

select * from phone_location_info where city = '����'
select * from china_info where city_name like '%����%'
select * from china_info where county_name like '%����%'
select * from china_info where province_name = '�ຣ'

update phone_location_info  set city = '��������' where phone_code = '1362473'
update phone_location_info set china_id = null where china_id = -1;
update china_info set city_name = '����' where city_name='�����' and province_id = 26;