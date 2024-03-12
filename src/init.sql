CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "ClientesExtrato" (
    "ClienteId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Limite" integer NOT NULL,
    "Saldo" integer NOT NULL,
    CONSTRAINT "PK_ClientesExtrato" PRIMARY KEY ("ClienteId")
);

CREATE TABLE "Transactions" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "ClientId" integer NOT NULL,
    "Descricao" text NOT NULL,
    "Tipo" text NOT NULL,
    "Valor" integer NOT NULL,
    "RealizadaEm" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Transactions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Transactions_ClientesExtrato_ClientId" FOREIGN KEY ("ClientId") REFERENCES "ClientesExtrato" ("ClienteId") ON DELETE RESTRICT
);

CREATE INDEX "IX_Transactions_ClientId" ON "Transactions" ("ClientId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240311121140_initial', '8.0.2');

DO $$
    BEGIN
        INSERT INTO "ClientesExtrato" ("Saldo", "Limite")
        VALUES ( 0,   1000 * 100),
               ( 0,    800 * 100),
               ( 0,  10000 * 100),
               ( 0, 100000 * 100),
               ( 0,   5000 * 100);
    END;
$$;

COMMIT;