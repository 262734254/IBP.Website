select * from phone_location_info where china_id is null group by city

select top 1 * from phone_location_info where phone_code = '1392651'

update phone_location_info set city = '海西蒙古族藏族自治州' where city = '西海';
update phone_location_info set city = '乌兰查布', province='内蒙古' where city = '内蒙乌兰查布' and province = '内蒙乌兰查布';
select * from phone_location_info where city = '贺州';
select * from china_info where id = 493;

select * from phone_location_info where city = '西海'
select * from china_info where city_name like '%西海%'
select * from china_info where county_name like '%西海%'
select * from china_info where province_name = '青海'

update phone_location_info  set city = '阿拉善盟' where phone_code = '1362473'
update phone_location_info set china_id = null where china_id = -1;
update china_info set city_name = '阿里' where city_name='阿里地' and province_id = 26;