/*
900 rows
Bytes sent from client	1972
Bytes received from server	5.24909
 */

SELECT [p].[Id], [p].[Name], [t1].[CategoriesId], [t1].[ProductsId], [t1].[Id], [t1].[Name], [t1].[CategoriesId0], [t1].[TagsId], [t1].[Id0], [t1].[Name0]
FROM [Products] AS [p]
LEFT JOIN (
    SELECT [c].[CategoriesId], [c].[ProductsId], [c0].[Id], [c0].[Name], [t0].[CategoriesId] AS [CategoriesId0], [t0].[TagsId], [t0].[Id] AS [Id0], [t0].[Name] AS [Name0]
    FROM [CategoryProduct] AS [c]
    INNER JOIN [Categories] AS [c0] ON [c].[CategoriesId] = [c0].[Id]
    LEFT JOIN (
        SELECT [c1].[CategoriesId], [c1].[TagsId], [t].[Id], [t].[Name]
        FROM [CategoryTag] AS [c1]
        INNER JOIN [Tags] AS [t] ON [c1].[TagsId] = [t].[Id]
    ) AS [t0] ON [c0].[Id] = [t0].[CategoriesId]
) AS [t1] ON [p].[Id] = [t1].[ProductsId]
ORDER BY [p].[Id], [t1].[CategoriesId], [t1].[ProductsId], [t1].[Id], [t1].[CategoriesId0], [t1].[TagsId], [t1].[Id0]