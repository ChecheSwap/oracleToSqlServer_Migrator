CREATE OR REPLACE PROCEDURE GET_DDLVIEWS(GREF OUT SYS_REFCURSOR) AUTHID CURRENT_USER AS
BEGIN
    OPEN GREF FOR SELECT * FROM XVIEW_SCRIPTS;
END;

CREATE OR REPLACE PROCEDURE GET_REFCONS(GREF OUT SYS_REFCURSOR) AUTHID CURRENT_USER AS
BEGIN
    OPEN GREF FOR SELECT * FROM XCONSTRAINTS_REFERENCIALS;
END;

CREATE OR REPLACE PROCEDURE GET_DML(GRES OUT SYS_REFCURSOR) AUTHID CURRENT_USER AS
BEGIN
    OPEN GRES FOR SELECT * FROM XDML_ALL;
END;
CREATE OR REPLACE PROCEDURE GET_DDL(GRES OUT SYS_REFCURSOR) AUTHID CURRENT_USER AS
BEGIN
    OPEN GRES FOR SELECT * FROM XDDL_TABLE;
END;

CREATE OR REPLACE PROCEDURE SP_FIRST(USR VARCHAR2)  AUTHID CURRENT_USER  AS
   
BEGIN    
    XPROC_DDL_FILTERS;
    XPROC_DATATYPES_MSSQL;
    XPROC_METADATA_ALL(USR);       
    XPROC_START_DML_ACTIONS(USR);    
END;

CREATE OR REPLACE PROCEDURE SP_SECOND  AUTHID CURRENT_USER  AS
BEGIN
    XPROC_SYNTHETIZE_ALL;
END;


CREATE OR REPLACE PROCEDURE SP_GET_USERNAME(GRES OUT SYS_REFCURSOR) AS
BEGIN
    OPEN GRES FOR SELECT USERNAME FROM ALL_USERS WHERE USERNAME NOT IN('XS$NULL', 
                                                                        'USR_EXPORT',
                                                                        'APEX_040000',
                                                                        'APEX_PUBLIC_USER',
                                                                        'FLOWS_FILES',                                                                        
                                                                        'MDSYS',
                                                                        'ANONYMOUS',
                                                                        'XDB',
                                                                        'CTXSYS',
                                                                        'APPQOSSYS',
                                                                        'DBSNMP',
                                                                        'ORACLE_OCM',
                                                                        'DIP',
                                                                        'OUTLN',
                                                                        'SYSTEM',
                                                                        'SYS'); 
END;

CREATE OR REPLACE PROCEDURE SP_GET_TABLE_LIST(GRES OUT SYS_REFCURSOR, USRNAME IN VARCHAR2) AS
BEGIN
    OPEN GRES FOR SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER = USRNAME;
END;

CREATE OR REPLACE PROCEDURE SP_GET_VIEW_LIST(GRES OUT SYS_REFCURSOR, USRNAME IN VARCHAR2) AS
BEGIN
    OPEN GRES FOR SELECT VIEW_NAME FROM ALL_VIEWS WHERE OWNER = USRNAME;
END;

