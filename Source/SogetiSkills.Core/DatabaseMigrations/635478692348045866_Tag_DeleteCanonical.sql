CREATE PROCEDURE Tag_DeleteCanonical
(
	@id int
)
AS

UPDATE Tags
SET IsCanonical = 0
WHERE ID = @id;