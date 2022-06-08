IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'T_Catalogue')
BEGIN
    CREATE TABLE T_Catalogue (
        [Ref_ID] NVARCHAR(6) PRIMARY KEY,
        [Name] NVARCHAR(50),
        [Description] NVARCHAR(255)
    )
END
GO

-- sp_CreateCookie
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'sp_CreateCookie')
BEGIN
    DROP PROC sp_CreateCookie
END
GO

CREATE PROCEDURE sp_CreateCookie (
    @Ref_ID NVARCHAR(6),
    @Name NVARCHAR(50),
    @Description NVARCHAR(255)
)
AS
BEGIN
    DECLARE @TotalAffected INT = 0
    INSERT T_Catalogue (
        [Ref_ID],
        [Name],
        [Description]
    )
    SELECT [Ref_ID] = @Ref_ID,
        [Name] = @Name,
        [Description] = @Description
    WHERE NOT EXISTS (SELECT 1 FROM T_Catalogue WHERE [Ref_ID] = @Ref_ID)
    SET @TotalAffected = @@ROWCOUNT
    IF @TotalAffected = 0
        RETURN 0
    ELSE
        RETURN 1
END
GO

-- sp_DeleteCookie
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'sp_DeleteCookie')
BEGIN
    DROP PROC sp_DeleteCookie
END
GO

CREATE PROCEDURE sp_DeleteCookie (
    @Ref_ID NVARCHAR(6)
)
AS
BEGIN
    DECLARE @TotalAffected INT = 0
    DELETE FROM T_Catalogue
    WHERE [Ref_ID] = @Ref_ID
    SET @TotalAffected = @@ROWCOUNT
    IF @TotalAffected = 0
        RETURN 0
    ELSE
        RETURN 1
END
GO

-- sp_EditCookie
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'sp_EditCookie')
BEGIN
    DROP PROC sp_EditCookie
END
GO

CREATE PROCEDURE sp_EditCookie (
    @Ref_ID NVARCHAR(6),
    @Name NVARCHAR(50),
    @Description NVARCHAR(255)
)
AS
BEGIN
    DECLARE @TotalAffected INT = 0
    UPDATE x SET
        [Name] = @Name,
        [Description] = @Description
    FROM T_Catalogue x
    WHERE [Ref_ID] = @Ref_ID
    SET @TotalAffected = @@ROWCOUNT
    IF @TotalAffected = 0
        RETURN 0
    ELSE
        RETURN 1
END
GO

-- Insert initial data
INSERT T_Catalogue
SELECT [Ref_ID] = 'CC0001', [Name] = 'Kue bulat', [Description] = 'Kue Bulat terbuat dari tepung'
WHERE NOT EXISTS (SELECT 1 FROM T_Catalogue WHERE [Ref_ID] = 'CC0001')
UNION
SELECT [Ref_ID] = 'CC0002', [Name] = 'Kue persegi panjang', [Description] = 'Kue persegi panjang terbuat dari beras'
WHERE NOT EXISTS (SELECT 1 FROM T_Catalogue WHERE [Ref_ID] = 'CC0002')
UNION
SELECT [Ref_ID] = 'CC0003', [Name] = 'Kue asik', [Description] = 'Kue asik terbuat dari telur'
WHERE NOT EXISTS (SELECT 1 FROM T_Catalogue WHERE [Ref_ID] = 'CC0003')