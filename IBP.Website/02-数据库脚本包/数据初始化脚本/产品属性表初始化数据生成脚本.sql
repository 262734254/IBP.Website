
SELECT 'INSERT INTO [ibp_db].[dbo].[product_category_attributes] ([category_attribute_id],[product_category_id],[attribute_name],[node_id],[parent_node],[group_name],[is_display],[field_type],[field_min_length],[field_max_length],[custom_value],[default_value],[is_request],[sort_order],[status],[description],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES (''' + 
  [category_attribute_id] + ''',''' +
  [product_category_id] + ''',''' + 
  [attribute_name] + ''',' +
  '0,' +									-- [node_id],
  '0,''' +									-- [parent_node],
  [group_name] + ''',' +
  '0,''' +									-- [is_display],
  [field_type] + ''',' +
  case when field_min_length = null then '-1' else cast(field_min_length as varchar(50)) end + ',' +
  case when field_max_length = null then '-1' else cast(field_max_length as varchar(50)) end + ',''' + 
  case when custom_value is null then 'NULL' else cast(custom_value as varchar(2000)) end + ''',''' + 
  case when [default_value] is null then 'NULL' else CAST([default_value] as varchar(2000)) end + ''',' +
  cast([is_request] as varchar(50)) + ',' + 
  cast([sort_order] as varchar(50)) + ',' +
  cast([status] as varchar(50)) + ',''' +
  convert(varchar(2000), [description]) + ''',''' +
  convert(varchar,[created_on],25) + ''',''' + 
  [created_by] + ''',''' + 
  convert(varchar, [modified_on],25) + ''',''' + 
  case when [modified_by] is null then 'NULL' else + [modified_by] end + ''',' +
  cast([status_code] as varchar(50)) + 
  ')' 
  FROM [ibp_db].[dbo].[product_category_attributes]
GO


