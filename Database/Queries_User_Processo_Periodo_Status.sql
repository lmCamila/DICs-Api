/*Usuários*/
SELECT u.*, e.*, p.* FROM users u INNER JOIN empreendimento e ON u.idEmpreendimento = e.id
							      INNER JOIN processo p ON u.idProcesso = p.id
								  WHERE u.removed = 0

SELECT u.*, e.*, p.* FROM users u INNER JOIN empreendimento e ON u.idEmpreendimento = e.id
							      INNER JOIN processo p ON u.idProcesso = p.id
					  		      WHERE u.id = @Id AND u.remmoved = 0

INSERT INTO users (name, avatar, email, pass, idEmpreendimento, idProcesso, isLiderEmpreendimento, isLiderProcesso , removed)
		    VALUES(@Name, @Avatar, @Email, @Pass, @IdEmpreendimento, @IdProcesso, @IsLiderEmpreendimento, @IsLiderProcesso, 0)

UPDATE users SET removed = 1 WHERE id = @Id

UPDATE users SET name = @Name, avatar = @Avatar, email = @Email, pass = @Pass, idEmpreendimento = @IdEmpreendimento, idProcesso = @IdProcesso, isLiderEmpreendimento = @IsLiderEmpreendimento, isLiderProcesso = @IsLiderProcesso WHERE id = @Id


/*Status*/
SELECT * FROM status_desafio WHERE removed = 0

/*Período*/
SELECT * FROM periodo WHERE removed = 0

INSERT INTO periodo (qtd_meses, name) VALUES(@Qtd, @Name)

UPDATE periodo SET removed = 1 WHERE id = @Id

UPDATE periodo SET qtd_meses = @Qtd, name = @Name WHERE id = @Id

/*Processo*/
SELECT p.*, e.* FROM processo p INNER JOIN empreendimento e ON p.idEmpreendimento = e.id WHERE p.removed = 0

SELECT p.*, e.* FROM processo p INNER JOIN empreendimento e ON p.idEmpreendimento = e.id WHERE p.id = @Id AND p.removed = 0

SELECT u.* FROM users u INNER JOIN processos p ON u.idProcesso = p.id WHERE p.id = @Id AND u.removed = 0

INSERT INTO processo (name, idEmpreendimento) VALUES (@Name, @IdEmpreendimento)

UPDATE processo SET removed = 1 WHERE id = @Id

UPDATE processo SET name = @Name, idEmpreendimento = @IdEmpreendimento WHERE id = @Id

