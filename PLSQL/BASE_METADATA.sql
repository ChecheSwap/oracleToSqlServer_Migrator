CREATE OR REPLACE PROCEDURE XPROC_DDL_FILTERS AUTHID CURRENT_USER AS ---FILTROS DE SALIDA DBMS
BEGIN
        EXECUTE IMMEDIATE '         
        BEGIN
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''STORAGE'',FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''TABLESPACE'',FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''SEGMENT_ATTRIBUTES'', FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''REF_CONSTRAINTS'', FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''CONSTRAINTS'', TRUE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''EMIT_SCHEMA'',FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''SEGMENT_CREATION'',FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''CONSTRAINTS_AS_ALTER'',FALSE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''SQLTERMINATOR'', TRUE);
            DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,''PRETTY'', TRUE);
        
        END;';
        
        EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20003, 'Error en proceso suscriptor de interpolacion de metadatos. '||SQLERRM);
END;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE XPROC_DATATYPES_MSSQL AUTHID CURRENT_USER AS --CATALOGO TIPOS + RESERVADAS
    TABNAME VARCHAR2(30):= 'XTABLE_DATATYPES_CATALOGUE';
BEGIN
    
    BEGIN    
        EXECUTE IMMEDIATE 'DROP TABLE '||TABNAME;
        EXCEPTION WHEN OTHERS THEN NULL;    
    END;
    
    EXECUTE IMMEDIATE 'CREATE TABLE '|| TABNAME ||' (ORATYPE VARCHAR2(100), MSSQLTYPE VARCHAR2(200))';        
    
    EXECUTE IMMEDIATE 
    '
     BEGIN
     INSERT INTO '||TABNAME||' VALUES(''NUMBER(19)'',''BIGINT'');
     INSERT INTO '||TABNAME||' VALUES(''RAW'',''BINARY'');
     INSERT INTO '||TABNAME||' VALUES(''NUMBER(3)'',''BIT'');
     INSERT INTO '||TABNAME||' VALUES(''CHAR'',''CHAR'');
     INSERT INTO '||TABNAME||' VALUES(''DATE'',''DATE'');
     INSERT INTO '||TABNAME||' VALUES(''NUMBER'',''DECIMAL'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''LONG RAW'',''IMAGE'');
     INSERT INTO '||TABNAME||' VALUES(''NUMBER(10)'',''INTEGER'');
     INSERT INTO '||TABNAME||' VALUES(''NUMBER(19,4)'',''MONEY'');
     INSERT INTO '||TABNAME||' VALUES(''NCHAR'',''NCHAR'');
     INSERT INTO '||TABNAME||' VALUES(''LONG'',''NTEXT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(23)'',''REAL'');
     INSERT INTO '||TABNAME||' VALUES(''LONG'',''TEXT'');
     INSERT INTO '||TABNAME||' VALUES(''NUMBER(3)'',''TINYINT'');
     INSERT INTO '||TABNAME||' VALUES(''VARCHAR2'',''VARCHAR'');   
     INSERT INTO '||TABNAME||' VALUES(''ENABLE'','''');  
     INSERT INTO '||TABNAME||' VALUES(''TO_CHAR('',''CONVERT(VARCHAR,'');
     INSERT INTO '||TABNAME||' VALUES(''CHAR)'','')'');
     INSERT INTO '||TABNAME||' VALUES(''INTERVAL YEAR (2) TO MONTH'',''VARCHAR(30)'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(54)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(55)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(56)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(57)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(58)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(59)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(60))'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(61)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(62)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(63)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(64)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(65)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(66)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(67)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(68)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(69)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(70)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(71)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(72)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(73)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(74)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(75)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(76)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(77)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(78)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(79)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(80)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(81)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(82)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(83)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(84)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(85)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(86)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(100)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''FLOAT(126)'',''FLOAT'');
     INSERT INTO '||TABNAME||' VALUES(''TIMESTAMP (6) WITH LOCAL TIME ZONE'',''VARCHAR(50)'');
     END;';
     
     EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20000, 'Error en proceso generalizador de catalogo de tipos de datos.'||SQLERRM);
    
END;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE XPROC_METADATA_ALL(USR VARCHAR2) AUTHID CURRENT_USER AS --SCRIPT ORACLE OBJETOS
BEGIN
    DBMS_OUTPUT.PUT_LINE(USR);
    BEGIN --OBJETOS TABLAS
        BEGIN
            EXECUTE IMMEDIATE 'DROP TABLE XTABLE_SCRIPTS';
            EXCEPTION WHEN OTHERS THEN NULL;        
        END;
        EXECUTE IMMEDIATE 'CREATE TABLE XTABLE_SCRIPTS AS SELECT TO_CHAR(DBMS_METADATA.GET_DDL( ''TABLE'',TABLE_NAME,'''||USR||''')) SCRIPT FROM ALL_TABLES WHERE OWNER = '''||USR||'''';   
        
        EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20000, 'Error en proceso generalizador de Script DDL para Tablas. '||SQLERRM);
    END;
    COMMIT;
    BEGIN    --REF CONSTRAINTS
    
        BEGIN
            EXECUTE IMMEDIATE 'DROP TABLE XCONSTRAINTS_REFERENCIALS';
            EXCEPTION WHEN OTHERS THEN NULL;
        END;
        
        EXECUTE IMMEDIATE '
                CREATE TABLE XCONSTRAINTS_REFERENCIALS AS
                SELECT TO_CHAR(DBMS_METADATA.GET_DEPENDENT_DDL(''REF_CONSTRAINT'', TABLE_NAME, '''||USR||''')) DDLSCRIPT FROM ALL_TABLES T
                WHERE EXISTS (SELECT 33 FROM ALL_CONSTRAINTS WHERE TABLE_NAME = T.TABLE_NAME AND OWNER = '''||USR||''' AND CONSTRAINT_TYPE = ''R'')
        ';    
                 
        EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20001, 'Error en proceso generalizador de Restricciones referenciales. '||SQLERRM);
    END;   
    COMMIT;
    DECLARE -- OBJETOS VISTAS
        TMP VARCHAR2(4000);
        TMPLONG LONG;
        TNAME VARCHAR2(200):='XVIEW_SCRIPTS';
        
        XC VARCHAR2(200);
    BEGIN    
        
        BEGIN
            EXECUTE IMMEDIATE 'DROP TABLE '||TNAME;
            EXCEPTION WHEN OTHERS THEN NULL;
        END;
        
        EXECUTE IMMEDIATE 'CREATE TABLE '||TNAME||' ("SCRIPT" VARCHAR2(4000))';
        
        
        FOR X IN (SELECT VIEW_NAME, TEXT FROM ALL_VIEWS WHERE OWNER = ''||USR||'') LOOP
            TMP := SUBSTR(X.TEXT, 1, 3200);
            
            BEGIN                    
                EXECUTE IMMEDIATE ('INSERT INTO '||TNAME||' VALUES(''CREATE VIEW '||X.VIEW_NAME||' AS '||TMP||';'')');
                EXCEPTION WHEN OTHERS THEN DBMS_OUTPUT.PUT_LINE(SQLERRM);        
            END;
        END LOOP;    
        
        EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20002, 'Error en proceso generalizador de Script DDL para Vistas. '||SQLERRM);
    END;    
    COMMIT;
    DECLARE   ---NONCLUSTERED INDEX FOR NULL
        CURSOR CC IS SELECT AC.CONSTRAINT_NAME CONSNAME, AC.TABLE_NAME TABNAME, CCOL.COLUMN_NAME COLNAME FROM ALL_CONSTRAINTS AC  INNER JOIN 
            ALL_CONS_COLUMNS CCOL ON(AC.CONSTRAINT_NAME = CCOL.CONSTRAINT_NAME);   
            
        TMPVAL CC%ROWTYPE;    
        TYPE CCR IS REF CURSOR;    
        XCUR CCR;    
        TXCUR CCR;    
        TNAME VARCHAR2(300);
        
        SQLQINDEX VARCHAR2(40):= 'CREATE UNIQUE NONCLUSTERED INDEX ';
        SQLQDROP VARCHAR2(30):= 'ALTER TABLE ';
        SQLQDO VARCHAR2(200):= SQLQDROP;
    BEGIN
        
        OPEN TXCUR FOR 'SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER = :D' USING USR;
        LOOP
            
            FETCH TXCUR INTO TNAME;
            
            EXIT WHEN TXCUR%NOTFOUND;
            
            OPEN XCUR FOR 'SELECT AC.CONSTRAINT_NAME CONSNAME, AC.TABLE_NAME TABNAME, CCOL.COLUMN_NAME COLNAME FROM ALL_CONSTRAINTS AC  INNER JOIN 
                ALL_CONS_COLUMNS CCOL ON(AC.CONSTRAINT_NAME = CCOL.CONSTRAINT_NAME) WHERE AC.OWNER = '''||USR||''' AND AC.TABLE_NAME = '''||TNAME||''' AND 
                CONSTRAINT_TYPE = ''U'' AND AC.SEARCH_CONDITION IS NULL';                              
                LOOP                        
                
                FETCH XCUR INTO TMPVAL;
                EXIT WHEN XCUR%NOTFOUND;
                
                SQLQDO := SQLQDO || TMPVAL.TABNAME || ' DROP CONSTRAINT '||TMPVAL.CONSNAME||';';
                EXECUTE IMMEDIATE 'INSERT INTO XTABLE_SCRIPTS VALUES(:N)' USING SQLQDO;
                
                SQLQDO := SQLQINDEX;            
                SQLQDO := SQLQDO || TMPVAL.CONSNAME||' ON '||TMPVAL.TABNAME||'('||TMPVAL.COLNAME||') WHERE '||TMPVAL.COLNAME|| ' IS NOT NULL;';
                EXECUTE IMMEDIATE 'INSERT INTO XTABLE_SCRIPTS VALUES(:M)' USING SQLQDO;
                SQLQDO := SQLQDROP;
                
            END LOOP;
        END LOOP;
        
        EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20002, 'Error en proceso de reemplazo de indices para Tablas. '||SQLERRM);
    END;  
    COMMIT;
END;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE XPROC_START_DML_ACTIONS (USR VARCHAR2) AUTHID CURRENT_USER AS    ---ALL DML
   
   TDMLNAME VARCHAR2(20):= 'XDML_ALL';    
    
    FUNCTION GEN_DML(TNAME VARCHAR2, TSAVE VARCHAR2, USR VARCHAR2) RETURN BOOLEAN  AS    
        FLAG BOOLEAN := FALSE;
        MYCUR INTEGER DEFAULT DBMS_SQL.OPEN_CURSOR;     
        COL_VAL VARCHAR2(4000); 
        STATUS INTEGER;
        
        TAB_DESC DBMS_SQL.DESC_TAB; 
        COL_COUNTER number; 
        SQLQ VARCHAR2(32000); 
        
        SQLDML_BASE VARCHAR2(32000):= 'INSERT INTO '||TNAME||' VALUES(';
        SQLDML VARCHAR2(32000) := SQLDML_BASE;
        
        TDMLNAME VARCHAR2(20):= TSAVE;
        
        ORATYPE INTEGER;
        
        BEGIN    
            BEGIN
                SQLQ := 'SELECT * FROM '||USR||'.'||TNAME; 
                
                DBMS_SQL.PARSE(MYCUR,SQLQ,DBMS_SQL.NATIVE);         
                DBMS_SQL.DESCRIBE_COLUMNS(MYCUR,COL_COUNTER, TAB_DESC); 
                
                FOR X IN 1 .. COL_COUNTER LOOP 
                    DBMS_SQL.DEFINE_COLUMN(MYCUR, X, COL_VAL, 4000); 
                END LOOP;
                
                STATUS := DBMS_SQL.EXECUTE(MYCUR); 
                
                WHILE ( 0 < DBMS_SQL.FETCH_ROWS(MYCUR) ) LOOP 
                        FOR X IN 1 .. COL_COUNTER LOOP         
                        
                        DBMS_SQL.COLUMN_VALUE(MYCUR, X, COL_VAL);                         
                        
                        ORATYPE := TAB_DESC(X).COL_TYPE;
                        
                        CASE 
                            WHEN  ORATYPE <> 2 AND ORATYPE <> 100 
                            /*OR ORATYPE = 9 
                            OR ORATYPE = 96 
                            OR ORATYPE = 12
                            OR ORATYPE = 286
                            OR ORATYPE = 287
                            OR ORATYPE = 187
                            OR ORATYPE = 188
                            OR ORATYPE = 232
                            OR ORATYPE = 189
                            OR ORATYPE = 190*/
                            THEN          
                                COL_VAL := REPLACE(COL_VAL,'''','');
                                SQLDML := SQLDML || '''' || COL_VAL || '''';                          
                            ELSE
                                SQLDML := SQLDML || COL_VAL;
                        END CASE;                    
                                                                   
                        IF X = COL_COUNTER THEN
                            SQLDML := SQLDML || ');';
                        ELSE
                            SQLDML := SQLDML || ',';
                        END IF;
                        
                        SQLDML := REPLACE(SQLDML, '''''', 'NULL');
                        SQLDML := REPLACE(SQLDML, '(,', '(NULL,');
                        SQLDML := REPLACE(SQLDML, ',)', ',NULL)');
                        SQLDML := REPLACE(SQLDML, ',,', ',NULL,');
                    END LOOP;
                    
                    SQLDML := REPLACE(SQLDML, '''', '''''');
                                        
                    EXECUTE IMMEDIATE 'INSERT INTO '||TDMLNAME||' VALUES('''||SQLDML||''')';
                    
                    SQLDML := SQLDML_BASE;
            END LOOP; 
                
                DBMS_SQL.CLOSE_CURSOR(MYCUR); 
                
                FLAG := NOT FLAG;
                
                EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20010, 'Error en proceso generalizador de Script DML Inserts. '||SQLERRM);
                --EXCEPTION WHEN OTHERS THEN DBMS_OUTPUT.PUT_LINE(SQLERRM);
            END;
            RETURN FLAG;
        END GEN_DML;        
BEGIN

    BEGIN
        EXECUTE IMMEDIATE 'DROP TABLE '||TDMLNAME;        
        EXCEPTION WHEN OTHERS THEN NULL;    
    END;
                
    BEGIN                
        EXECUTE IMMEDIATE 'CREATE TABLE '||TDMLNAME||' ("SENTENCE" VARCHAR2(4000))';
        EXCEPTION WHEN OTHERS THEN NULL;    
    END;       
    
    FOR X IN (SELECT TABLE_NAME TN FROM ALL_TABLES WHERE OWNER = USR) LOOP
        IF GEN_DML(X.TN, TDMLNAME, USR) THEN
            NULL;        
        END IF;     
    END LOOP;   
           
    EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20011, 'Error en proceso generalizador externo de Script DML Inserts. '||SQLERRM);
    --EXCEPTION WHEN OTHERS THEN DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE XPROC_SYNTHETIZE_ALL AUTHID CURRENT_USER AS
    TMP VARCHAR2(4000);
    TNAME VARCHAR2(5000) := 'XDDL_TABLE';
BEGIN    
    
    BEGIN
        EXECUTE IMMEDIATE 'DROP TABLE '||TNAME;
        EXCEPTION WHEN OTHERS THEN NULL;        
    END;
    
    BEGIN
        EXECUTE IMMEDIATE 'CREATE TABLE '||TNAME||' ("MSSCRIPT" VARCHAR2(4000))';
        EXCEPTION WHEN OTHERS THEN NULL;
    END;
    
    FOR SC IN (SELECT SCRIPT FROM XTABLE_SCRIPTS) LOOP          
        FOR X IN (SELECT * FROM XTABLE_DATATYPES_CATALOGUE) LOOP     
        
            SC.SCRIPT := REPLACE(SC.SCRIPT,' '||X.ORATYPE, ' '||X.MSSQLTYPE);                
        END LOOP;
                
        EXECUTE IMMEDIATE 'INSERT INTO '||TNAME||' VALUES('''||SC.SCRIPT||''')';       
        
    END LOOP;
    
    EXECUTE IMMEDIATE 'UPDATE XCONSTRAINTS_REFERENCIALS SET DDLSCRIPT = REPLACE(DDLSCRIPT, '' ENABLE'' , '' '')';
        
    EXCEPTION WHEN OTHERS THEN RAISE_APPLICATION_ERROR(-20015, 'Error en proceso de sintesis del DDL ORA->MSSQL '||SQLERRM);
END;








