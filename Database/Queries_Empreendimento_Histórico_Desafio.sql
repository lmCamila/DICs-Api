/*EMPREENDIMENTO 
inserir*/ 
INSERT INTO DEPARTMENT(NAME) VALUES(@NAME);

--remover
UPDATE DEPARTMENT SET REMOVED = 1 WHERE ID = @id;

--update
UPDATE DEPARTMENT SET NAME = @nome;
--todos empreendimentos
SELECT * FROM DEPARTMENT WHERE REMOVED = 0 ;
		
--empreendimento e os processos 
SELECT p.* , u.* FROM PROCESS p INNER JOIN DEPARTMENT d 
					ON p.ID_DEPARTMENT = d.ID 
					INNER JOIN USERS u ON u.ID_DEPARTMENT = d.ID 
WHERE u.IS_LEADER_DEPARTMENT = 1 AND u.REMOVED = 0;
		
--se não tiver processos usuarios
SELECT u.* FROM USERS u INNER JOIN DEPARTMENT d ON u.ID_DEPARTMENT = d.ID 
WHERE u.REMOVED = 0
		
--usuários que estão nesse empreendimento 
SELECT d.* , u.* FROM USERS u INNER JOIN DEPARTMENT d 
						ON u.ID_DEPARTMENT = d.ID
WHERE u.REMOVED = 0
		
/*DESAFIOS
insert*/
INSERT INTO DIC(ID_USER, DESCRIPTION, START_DATE, END_DATE, FINISHED_DATE,
 ID_STATUS, ID_PERIOD) 
VALUES (@idUsuario, @descricao, @data_inset, @data_prevista_conclu,
		 @data_conclu, @idStatus, @idPeriod)

--update
UPDATE DIC SET ID_USER = @idUsuario, 
				DESCRIPTION = @descricao,
				START_DATE = @data_inset ,
				END_DATE = @data_prevista_conclu, 
				FINISHED_DATE = @data_conclu,
				ID_STATUS = @idStatus) 
WHERE ID = @Id 
							
--update status
UPDATE DIC SET ID_STATUS = @idStatus WHERE ID = @Id
		
--update descricao
UPDATE DIC SET DESCRIPTION = @Desc WHERE ID = @Id
--UM DESAFIO 
SELECT d.* FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
				INNER JOIN STATUS s ON d.ID_STATUS = s.ID
WHERE d.ID = @IdDic

--os desafios por empreendimento
SELECT d.* FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
				INNER JOIN STATUS s ON d.ID_STATUS = s.ID
WHERE u.ID_DEPARTMENT = @Id

--selecionar ultimo cadastrado
SELECT d.*, u.*, s.*, p.*, dep.*, pro.* 
FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
INNER JOIN STATUS s ON d.ID_STATUS = s.ID 
INNER JOIN PERIOD p ON d.ID_PERIOD = p.ID
INNER JOIN DEPARTMENT dep ON u.ID_DEPARTMENT = dep.ID
INNER JOIN PROCESS pro ON u.ID_PROCESS = pro.ID
WHERE d.ID = IDENT_CURRENT('DIC')

--os desafios por processo
SELECT d.* FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
				INNER JOIN STATUS s ON d.ID_STATUS = s.ID
WHERE u.ID_PROCESS = @IdProcess

--os desafios por usuários
SELECT d.* FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
				INNER JOIN STATUS s ON d.ID_STATUS = s.ID
WHERE d.ID_USER = @IdUser

--Histórico 
-- histórico de um desafio

SELECT h.* FROM DIC_HISTORY h INNER JOIN DIC d ON h.ID_DIC = d.ID
WHERE d.ID = @Id;

