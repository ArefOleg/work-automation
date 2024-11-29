PROCEDURE generate_trans_doc_csv(start_dt VARCHAR2,end_dt VARCHAR2, loy_program_id VARCHAR2, part_name VARCHAR2, year_ VARCHAR2, mounth VARCHAR2) 
    AS
            F UTL_FILE.FILE_TYPE;
            order_num_var varchar2(30 char):='';
            standart_point_id varchar2(15 char):='1-P80ZI9';
            long_point_id varchar2(15 char):='1-VTK3JY';
            short_point_id varchar2(15 char):='1-VVPYNK';
    CURSOR C1 IS 
    
              SELECT
'' AS SKU,
'' AS PROD_NAME,
so.order_num AS ORDER_NUMBER,
so.status_cd AS order_status,
so.x_card_number AS card_num,
so.job_type_cd AS job_type_cd,
so.created AS CREATED,
'' AS RRN,
so.total_net_pri AS order_total,
(so.total_net_pri-so.x_discount_cur) AS order_total_w_discount,
so.x_discount_cur AS order_discount,
'' AS AZS,
'' AS Terminal,
'' AS item_num,
'' AS item_net_price,
'' AS quantity,
'' AS item_price,
'' AS teboil_base_ntu,
'' AS teboil_base_fuel,          
'' AS teboil_power,
'' AS promo_long_accrul_val,
'' AS promo_long_accrul_date,
'' AS promo_short_accrul_val,
'' AS promo_short_accrul_date,
---- Списание баллов, через ручные корректировки
(
SELECT
/*+ index(T1 S_LOY_TXN_F15) index(T6 S_LOY_ACRL_DTL_I2)*/
SUM(T1.Loan_Balance)
FROM SIEBEL.S_LOY_TXN T1              
WHERE T1.TYPE_CD='Redemption'
AND T1.ORIG_ORDER_ID = so.row_id
) AS RED_POINTS,
---- Начисление баллов через Ручные корректировки  Процессинг чека
CASE
WHEN so.job_type_cd = 'Ручная корректировка' THEN
(SELECT
/*+ index(T1 S_LOY_TXN_F15) index(T6 S_LOY_ACRL_DTL_I2) index(T4 S_LOY_PROMO_F1)*/
SUM(T6.ACCRUED_VALUE) 
FROM SIEBEL.S_LOY_TXN T1,
SIEBEL.S_LOY_PROMO T4,
SIEBEL.S_LOY_ACRL_DTL T6
WHERE T6.TXN_ID           = T1.ROW_ID
AND T1.TYPE_CD='Accrual'
AND T6.PROMO_ID           = T4.ROW_ID(+)
AND T6.QUALIFY_TYPE_FLG   = 'N'
AND t4.LOY_PROG_ID      = loy_program_id
AND T1.ORIG_ORDER_ID = so.row_id
AND t4.NAME      NOT IN ('Teboil Base - Power', 'Teboil Base - Fuel', 'Teboil Base - NonFuel')
)
WHEN so.job_type_cd = 'Процессинг чека' THEN
(
SELECT
SUM(r_corr.x_ekto_net_total) + SUM (r_corr.x_fuel_net_total) + SUM(r_corr.x_nonfuel_net_total)
FROM SIEBEL.S_LOY_TXN r_corr
WHERE r_corr.orig_order_id = so.row_id
) END
AS accrul_val
--- 
FROM SIEBEL.S_ORDER so
WHERE 
so.x_card_number LIKE '703004%'
AND so.job_type_cd IN ('Ручная корректировка', 'Процессинг чека')
AND so.status_cd IN ('Обработан', 'Отменен')
AND so.created >= to_date(start_dt,'dd.mm.yyyy hh24:MI:SS')
AND so.created <= to_date(end_dt,'dd.mm.yyyy hh24:MI:SS')
ORDER BY  so.CREATED DESC;                           
            C1_R C1%ROWTYPE;
        BEGIN
            F := UTL_FILE.FOPEN('TEBOIL_REPORT','LOYTRX_'||year_||'_'||mounth||'_'||part_name||'.CSV','w',32767); 
            UTL_FILE.PUT(F,'Кассовый чек');
            UTL_FILE.PUT(F,','||'RRN');
            UTL_FILE.PUT(F,','||'Номер карты');
            UTL_FILE.PUT(F,','||'Дата чека');
            UTL_FILE.PUT(F,','||'Тип чека');
            UTL_FILE.PUT(F,','||'АЗС');
            UTL_FILE.PUT(F,','||'Терминал');
            UTL_FILE.PUT(F,','||'Сумма чека без скидки. руб');
            UTL_FILE.PUT(F,','||'Сумма чека со скидкой. руб');
            UTL_FILE.PUT(F,','||'Скидка чека. руб');
            UTL_FILE.PUT(F,','||'Статус чека');
            UTL_FILE.PUT(F,','||'Товар');
            UTL_FILE.PUT(F,','||'Код товара ');
            UTL_FILE.PUT(F,','||'Номер позиции');
            UTL_FILE.PUT(F,','||'Стоимость единицы. руб');
            UTL_FILE.PUT(F,','||'Количество единиц');
            UTL_FILE.PUT(F,','||'Полная цена за позицию с учетом количества. руб');
            UTL_FILE.PUT(F,','||'Teboil Promo');
            UTL_FILE.PUT(F,','||'Списано баллов');
            UTL_FILE.PUT(F,','||'Teboil Base - NonFuel');
            UTL_FILE.PUT(F,','||'Teboil Base - Fuel');
            UTL_FILE.PUT(F,','||'Teboil Base - Power');
            UTL_FILE.PUT(F,','||'Списано баллов');
            UTL_FILE.PUT(F,','||'Отложеннные баллы 1');
            UTL_FILE.PUT(F,','||'Дата доступности отложенных баллов 1');
            UTL_FILE.PUT(F,','||'Отложеннные баллы 2');
            UTL_FILE.PUT(F,','||'Дата доступности отложенных баллов 2');
            UTL_FILE.NEW_LINE(F);
            FOR C1_R IN C1
            LOOP
                
                UTL_FILE.PUT(F,C1_R.ORDER_NUMBER);
                UTL_FILE.PUT(F,','||C1_R.RRN);
                UTL_FILE.PUT(F,','||C1_R.card_num);  
                UTL_FILE.PUT(F,','||to_char(C1_R.CREATED + 3/24, 'dd.mm.yyyy hh24:MI:SS'));
                UTL_FILE.PUT(F,','||C1_R.job_type_cd);
                UTL_FILE.PUT(F,','||C1_R.AZS);
                UTL_FILE.PUT(F,','||C1_R.Terminal);
                UTL_FILE.PUT(F,','||REPLACE(C1_R.order_total,',','.'));
                UTL_FILE.PUT(F,','||REPLACE(C1_R.order_total_w_discount,',','.'));           
                UTL_FILE.PUT(F,','||REPLACE(C1_R.order_discount,',','.'));     
                UTL_FILE.PUT(F,','||C1_R.order_status);
                UTL_FILE.PUT(F,','||REPLACE(C1_R.PROD_NAME,',','.'));                         
                UTL_FILE.PUT(F,','||C1_R.SKU); 
                UTL_FILE.PUT(F,','||C1_R.item_num);
                UTL_FILE.PUT(F,','||REPLACE(C1_R.item_net_price,',','.'));
                UTL_FILE.PUT(F,','||REPLACE(C1_R.quantity,',','.'));
                UTL_FILE.PUT(F,','||REPLACE(C1_R.item_price,',','.'));
                IF C1_R.ORDER_NUMBER = order_num_var THEN
                  UTL_FILE.PUT(F,','||'');  /*Начисление */
                  UTL_FILE.PUT(F,','||'');  /*Списание */
                  UTL_FILE.PUT(F,','||'');  /*Base NTU */
                  UTL_FILE.PUT(F,','||'');  /*Fuel */
                  UTL_FILE.PUT(F,','||'');  /*Power */
                  UTL_FILE.PUT(F,','||'');  /*Отложенные */
                  UTL_FILE.PUT(F,','||'');   /*Отложенные */
                  UTL_FILE.PUT(F,','||'');  /*Отложенные */
                  UTL_FILE.PUT(F,','||''); /*Отложенные */
                SELECT orders.order_num into order_num_var FROM SIEBEL.S_ORDER orders WHERE orders.order_num = C1_R.ORDER_NUMBER;
                       ELSE
                          SELECT orders.order_num into order_num_var FROM SIEBEL.S_ORDER orders WHERE orders.order_num = C1_R.ORDER_NUMBER;                        
                /* Убираем из общей балы за Teboil Base и Power */          
           
                UTL_FILE.PUT(F,','||(C1_R.accrul_val));
                UTL_FILE.PUT(F,','||REPLACE(C1_R.RED_POINTS,',','.'));
                UTL_FILE.PUT(F,','||C1_R.teboil_base_ntu); -- Teboil Base - NonFuel
                UTL_FILE.PUT(F,','||C1_R.teboil_base_fuel);-- Teboil Base - Fuel
                UTL_FILE.PUT(F,','||C1_R.teboil_power); -- Teboil Power
                UTL_FILE.PUT(F,','||C1_R.PROMO_LONG_ACCRUL_VAL); 
                UTL_FILE.PUT(F,','||C1_R.PROMO_LONG_ACCRUL_DATE); 
                UTL_FILE.PUT(F,','||C1_R.PROMO_SHORT_ACCRUL_VAL); 
                UTL_FILE.PUT(F,','||C1_R.PROMO_SHORT_ACCRUL_DATE); 
                END IF;               

                UTL_FILE.NEW_LINE(F);
            END LOOP;
            UTL_FILE.FCLOSE(F);
            COMMIT;
        END;