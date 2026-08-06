Request ID	TipoSolicitud	Line No_	Level	TipoOrigen	Node Name	Opciones	Comentario
IM_GET_DOCUMENT_LIST_LI	Solicitud	10000	1	Texto	Request		
IM_GET_DOCUMENT_LIST_LI	Solicitud	20000	2	Texto	Request_ID		
IM_GET_DOCUMENT_LIST_LI	Solicitud	30000	2	Texto	Request_Body		
IM_GET_DOCUMENT_LIST_LI	Solicitud	40000	3	Texto	HHT_ID		
IM_GET_DOCUMENT_LIST_LI	Solicitud	50000	3	Texto	Store_No		Text
IM_GET_DOCUMENT_LIST_LI	Solicitud	70000	3	Texto	Location_No		Text
IM_GET_DOCUMENT_LIST_LI	Solicitud	80000	3	Texto	Value_Type	[0,1,2] 0: Recolectores. 1: Recogidos 2: Verificadores.	Int
IM_GET_DOCUMENT_LIST_LI	Solicitud	90000	3	Texto	Value		
IM_GET_DOCUMENT_LIST_LI	Solicitud	100000	3	Texto	Process_Type		
IM_GET_DOCUMENT_LIST_LI	Solicitud	110000	3	Texto	Document_Type		
IM_GET_DOCUMENT_LIST_LI	Respuesta	110000	1	Texto	Response		
IM_GET_DOCUMENT_LIST_LI	Respuesta	120000	2	Texto	Request_ID		
IM_GET_DOCUMENT_LIST_LI	Respuesta	130000	2	Texto	Response_Code		
IM_GET_DOCUMENT_LIST_LI	Respuesta	140000	2	Texto	Response_Text		
IM_GET_DOCUMENT_LIST_LI	Respuesta	150000	2	Texto	Request_Body		
IM_GET_DOCUMENT_LIST_LI	Respuesta	160000	3	Tabla	POS_Trans._Inv._Header		
IM_GET_DOCUMENT_LIST_LI	Respuesta	170000	4	Campo	Transaction_No.		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	180000	4	Campo	Transaction_Type		Int
IM_GET_DOCUMENT_LIST_LI	Respuesta	190000	4	Campo	Document_No.		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	200000	4	Campo	Document_Member_Name		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	210000	4	Campo	Document_Type		Int
IM_GET_DOCUMENT_LIST_LI	Respuesta	220000	4	Campo	Document_No.		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	230000	4	Campo	P_R_Counting_Header_No		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	240000	4	Campo	Expected_Date		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	250000	4	Campo	No._of_Items		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	260000	4	Campo	Total_Qty		Int
IM_GET_DOCUMENT_LIST_LI	Respuesta	265000	4	Campo	Store_No.		Text
IM_GET_DOCUMENT_LIST_LI	Respuesta	270000	4	Campo	Qty_Left		Int
IM_SEND_DOCUMENT_LI	Solicitud	10000	1	Texto	Request		
IM_SEND_DOCUMENT_LI	Solicitud	20000	2	Texto	Request_ID		
IM_SEND_DOCUMENT_LI	Solicitud	30000	2	Texto	Request_Body		
IM_SEND_DOCUMENT_LI	Solicitud	40000	3	Texto	Update_Action		
IM_SEND_DOCUMENT_LI	Solicitud	50000	3	Tabla	Trans._Inventory_Header		
IM_SEND_DOCUMENT_LI	Solicitud	60000	4	Campo	Transaction_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	70000	4	Campo	Receipt_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	80000	4	Campo	Transaction_Type		Int
IM_SEND_DOCUMENT_LI	Solicitud	90000	4	Campo	Store_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	100000	4	Campo	POS_Terminal_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	110000	4	Campo	Staff_ID		Text
IM_SEND_DOCUMENT_LI	Solicitud	120000	4	Campo	Date		Text
IM_SEND_DOCUMENT_LI	Solicitud	130000	4	Campo	Time		Text
IM_SEND_DOCUMENT_LI	Solicitud	140000	4	Campo	Shift_No.		Int
IM_SEND_DOCUMENT_LI	Solicitud	150000	4	Campo	Shift_Date		Text
IM_SEND_DOCUMENT_LI	Solicitud	160000	4	Campo	Entry_Status		Int
IM_SEND_DOCUMENT_LI	Solicitud	170000	4	Campo	Start_Process		Text
IM_SEND_DOCUMENT_LI	Solicitud	180000	4	Campo	End_Process		Text
IM_SEND_DOCUMENT_LI	Solicitud	190000	4	Campo	Document_Member		Text
IM_SEND_DOCUMENT_LI	Solicitud	200000	4	Campo	Document_Type		Int
IM_SEND_DOCUMENT_LI	Solicitud	210000	4	Campo	Document_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	220000	4	Campo	Document_Member_Name		Text
IM_SEND_DOCUMENT_LI	Solicitud	230000	4	Campo	P_R_Counting_Header_No		Text
IM_SEND_DOCUMENT_LI	Solicitud	240000	4	Campo	Expected_Date		Text
IM_SEND_DOCUMENT_LI	Solicitud	250000	4	Campo	Replicated		Text
IM_SEND_DOCUMENT_LI	Solicitud	260000	4	Campo	Replication_Counter		Text
IM_SEND_DOCUMENT_LI	Solicitud	270000	3	Tabla	Trans._Inventory_Lines		
IM_SEND_DOCUMENT_LI	Solicitud	280000	4	Campo	Transaction_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	290000	4	Campo	Line_No.		Int
IM_SEND_DOCUMENT_LI	Solicitud	300000	4	Campo	Receipt_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	310000	4	Campo	Store_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	320000	4	Campo	POS_Terminal_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	330000	4	Campo	Staff_ID		Text
IM_SEND_DOCUMENT_LI	Solicitud	340000	4	Campo	Date		Text
IM_SEND_DOCUMENT_LI	Solicitud	350000	4	Campo	Time		Text
IM_SEND_DOCUMENT_LI	Solicitud	360000	4	Campo	Item_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	370000	4	Campo	Variant_Code		Text
IM_SEND_DOCUMENT_LI	Solicitud	380000	4	Campo	Barcode_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	390000	4	Campo	Item_Description		Text
IM_SEND_DOCUMENT_LI	Solicitud	400000	4	Campo	Item_Description_2		Text
IM_SEND_DOCUMENT_LI	Solicitud	410000	4	Campo	Quantity		Int
IM_SEND_DOCUMENT_LI	Solicitud	420000	4	Campo	Outstanding_Qty.		Int
IM_SEND_DOCUMENT_LI	Solicitud	430000	4	Campo	Unit_of_Measure		Int
IM_SEND_DOCUMENT_LI	Solicitud	440000	4	Campo	UOM_Qty.		Int
IM_SEND_DOCUMENT_LI	Solicitud	450000	4	Campo	Quantity__Base_		Int
IM_SEND_DOCUMENT_LI	Solicitud	460000	4	Campo	Outstanding_Qty.__Base_		Int
IM_SEND_DOCUMENT_LI	Solicitud	470000	4	Campo	Vendor_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	480000	4	Campo	Vendor_Item_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	490000	4	Campo	Serial_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	500000	4	Campo	Lot_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	510000	4	Campo	Expiration_Date		Text
IM_SEND_DOCUMENT_LI	Solicitud	520000	4	Campo	Item_Number_Scanned		Text
IM_SEND_DOCUMENT_LI	Solicitud	530000	4	Campo	Keyboard_Item_Entry		Text
IM_SEND_DOCUMENT_LI	Solicitud	540000	4	Campo	Replication_Counter		Text
IM_SEND_DOCUMENT_LI	Solicitud	550000	3	Tabla	Cab._Paletas		
IM_SEND_DOCUMENT_LI	Solicitud	560000	4	Campo	No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	570000	3	Tabla	Det._Paletas		
IM_SEND_DOCUMENT_LI	Solicitud	580000	4	Campo	Document_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	590000	4	Campo	Line_No.		Int
IM_SEND_DOCUMENT_LI	Solicitud	600000	4	Campo	Item_No.		Text
IM_SEND_DOCUMENT_LI	Solicitud	610000	4	Campo	Quantity		Int
IM_SEND_DOCUMENT_LI	Solicitud	620000	4	Campo	Unit_of_Measure_Code		Text
IM_SEND_DOCUMENT_LI	Solicitud	630000	3	Tabla	Detalle_Distribucion_Paleta		
IM_SEND_DOCUMENT_LI	Solicitud	640000	4	Campo	Line_Nos.		Int
IM_SEND_DOCUMENT_LI	Solicitud	650000	4	Campo	Cod._Producto		Text
IM_SEND_DOCUMENT_LI	Solicitud	660000	4	Campo	No._Transferencia		Text
IM_SEND_DOCUMENT_LI	Solicitud	670000	4	Campo	No._Paleta		Text
IM_SEND_DOCUMENT_LI	Solicitud	680000	4	Campo	Cantidad		Int
IM_SEND_DOCUMENT_LI	Solicitud	690000	4	Campo	No. Verificador		Text
IM_SEND_DOCUMENT_LI	Respuesta	550000	1	Texto	Response		
IM_SEND_DOCUMENT_LI	Respuesta	560000	2	Texto	Request_ID		
IM_SEND_DOCUMENT_LI	Respuesta	570000	2	Texto	Response_Code		
IM_SEND_DOCUMENT_LI	Respuesta	580000	2	Texto	Response_Text		
IM_SEND_DOCUMENT_LI	Respuesta	590000	2	Texto	Response_Body		
IM_GET_DOCUMENT_LI	Solicitud	10000	1	Texto	Request		
IM_GET_DOCUMENT_LI	Solicitud	20000	2	Texto	Request_ID		
IM_GET_DOCUMENT_LI	Solicitud	30000	2	Texto	Request_Body		
IM_GET_DOCUMENT_LI	Solicitud	40000	3	Texto	HHT_ID		
IM_GET_DOCUMENT_LI	Solicitud	50000	3	Texto	Document_Header		
IM_GET_DOCUMENT_LI	Solicitud	60000	3	Texto	Value_Type		
IM_GET_DOCUMENT_LI	Respuesta	10000	1	Texto	Response		
IM_GET_DOCUMENT_LI	Respuesta	20000	2	Texto	Request_ID		
IM_GET_DOCUMENT_LI	Respuesta	30000	2	Texto	Response_Code		
IM_GET_DOCUMENT_LI	Respuesta	40000	2	Texto	Response_Text		
IM_GET_DOCUMENT_LI	Respuesta	50000	2	Texto	Request_Body		
IM_GET_DOCUMENT_LI	Respuesta	60000	3	Tabla	POS Trans. Inv. Header		
IM_GET_DOCUMENT_LI	Respuesta	70000	4	Campo	Transaction No.		
IM_GET_DOCUMENT_LI	Respuesta	80000	4	Campo	Transaction Type		
IM_GET_DOCUMENT_LI	Respuesta	90000	4	Campo	Store No.		
IM_GET_DOCUMENT_LI	Respuesta	100000	4	Campo	POS Terminal No.		
IM_GET_DOCUMENT_LI	Respuesta	110000	4	Campo	Total Qty		
IM_GET_DOCUMENT_LI	Respuesta	120000	4	Campo	Qty Left		
IM_GET_DOCUMENT_LI	Respuesta	130000	4	Campo	Document Member		
IM_GET_DOCUMENT_LI	Respuesta	140000	4	Campo	Document Type		
IM_GET_DOCUMENT_LI	Respuesta	150000	4	Campo	Document No.		
IM_GET_DOCUMENT_LI	Respuesta	160000	4	Campo	Document Member Name		
IM_GET_DOCUMENT_LI	Respuesta	170000	4	Campo	P/R Counting Header No		
IM_GET_DOCUMENT_LI	Respuesta	180000	4	Campo	Expected Date		
IM_GET_DOCUMENT_LI	Respuesta	190000	4	Campo	No. Of Check Rounds		
IM_GET_DOCUMENT_LI	Respuesta	200000	4	Campo	Quantity Method		
IM_GET_DOCUMENT_LI	Respuesta	210000	4	Campo	Quick-default Quantity		
IM_GET_DOCUMENT_LI	Respuesta	220000	3	Tabla	POS Trans. Inv. Lines		
IM_GET_DOCUMENT_LI	Respuesta	230000	4	Campo	Transaction No.		
IM_GET_DOCUMENT_LI	Respuesta	240000	4	Campo	Line No.		
IM_GET_DOCUMENT_LI	Respuesta	250000	4	Campo	Store No.		
IM_GET_DOCUMENT_LI	Respuesta	260000	4	Campo	Item No.		
IM_GET_DOCUMENT_LI	Respuesta	270000	4	Campo	Variant Code		
IM_GET_DOCUMENT_LI	Respuesta	280000	4	Campo	Item Description		
IM_GET_DOCUMENT_LI	Respuesta	290000	4	Campo	Item Description 2		
IM_GET_DOCUMENT_LI	Respuesta	300000	4	Campo	Quantity		
IM_GET_DOCUMENT_LI	Respuesta	310000	4	Campo	Outstanding Qty.		
IM_GET_DOCUMENT_LI	Respuesta	320000	4	Campo	Unit of Measure		
IM_GET_DOCUMENT_LI	Respuesta	330000	4	Campo	UOM Qty.		
IM_GET_DOCUMENT_LI	Respuesta	340000	4	Campo	Quantity (Base)		
IM_GET_DOCUMENT_LI	Respuesta	350000	4	Campo	Outstanding Qty. (Base)		
IM_GET_DOCUMENT_LI	Respuesta	360000	4	Campo	Vendor No.		
IM_GET_DOCUMENT_LI	Respuesta	370000	4	Campo	Vendor Item No.		
IM_GET_DOCUMENT_LI	Respuesta	380000	4	Campo	Receive - Confirm Scan		
IM_GET_DOCUMENT_LI	Respuesta	460000	3	Tabla	Cab. Paletas		
IM_GET_DOCUMENT_LI	Respuesta	470000	4	Campo	No.		
IM_GET_DOCUMENT_LI	Respuesta	480000	4	Campo	No. Verificador		
IM_GET_DOCUMENT_LI	Respuesta	490000	4	Campo	No. Recolecta		
IM_GET_DOCUMENT_LI	Respuesta	500000	4	Campo	Status		
IM_GET_DOCUMENT_LI	Respuesta	510000	4	Campo	Total Documentos		
IM_GET_DOCUMENT_LI	Respuesta	520000	4	Campo	Posting Date		
IM_GET_DOCUMENT_LI	Respuesta	530000	3	Tabla	Det. Paletas		
IM_GET_DOCUMENT_LI	Respuesta	540000	4	Campo	Document No.		
IM_GET_DOCUMENT_LI	Respuesta	550000	4	Campo	Line No.		
IM_GET_DOCUMENT_LI	Respuesta	560000	4	Campo	Item No.		
IM_GET_DOCUMENT_LI	Respuesta	570000	4	Campo	Quantity		
IM_GET_DOCUMENT_LI	Respuesta	580000	4	Campo	Description		
IM_GET_DOCUMENT_LI	Respuesta	590000	4	Campo	Unit of Measure Code		
