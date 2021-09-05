/*
1300 rows
Bytes sent from client	2504
Bytes received from server	2.159134E+07
*/

SELECT [p].[Id], [p].[Name]
FROM [Products] AS [p]
ORDER BY [p].[Id]

SELECT [t].[CategoriesId], [t].[ProductsId], [t].[Id], [t].[Name], [p].[Id]
FROM [Products] AS [p]
INNER JOIN (
    SELECT [c].[CategoriesId], [c].[ProductsId], [c0].[Id], [c0].[Name]
    FROM [CategoryProduct] AS [c]
    INNER JOIN [Categories] AS [c0] ON [c].[CategoriesId] = [c0].[Id]
) AS [t] ON [p].[Id] = [t].[ProductsId]
ORDER BY [p].[Id], [t].[CategoriesId], [t].[ProductsId], [t].[Id]

SELECT [t1].[CategoriesId], [t1].[TagsId], [t1].[Id], [t1].[Name], [p].[Id], [t].[CategoriesId], [t].[ProductsId], [t].[Id]
FROM [Products] AS [p]
INNER JOIN (
    SELECT [c].[CategoriesId], [c].[ProductsId], [c0].[Id]
    FROM [CategoryProduct] AS [c]
    INNER JOIN [Categories] AS [c0] ON [c].[CategoriesId] = [c0].[Id]
) AS [t] ON [p].[Id] = [t].[ProductsId]
INNER JOIN (
    SELECT [c1].[CategoriesId], [c1].[TagsId], [t0].[Id], [t0].[Name]
    FROM [CategoryTag] AS [c1]
    INNER JOIN [Tags] AS [t0] ON [c1].[TagsId] = [t0].[Id]
) AS [t1] ON [t].[Id] = [t1].[CategoriesId]
ORDER BY [p].[Id], [t].[CategoriesId], [t].[ProductsId], [t].[Id]