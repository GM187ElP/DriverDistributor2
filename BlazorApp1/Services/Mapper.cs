namespace BlazorApp1.Services;

public static class Mapper
{
    public static void MapFromDto<TDto, TEntity>(TDto dto, TEntity entity)
    {
        var dtoProps = typeof(TDto).GetProperties();
        var entityProps = typeof(TEntity).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var entityProp = entityProps.FirstOrDefault(p =>
                p.Name == dtoProp.Name &&
                p.PropertyType == dtoProp.PropertyType &&
                p.CanWrite);

            if (entityProp != null)
            {
                var value = dtoProp.GetValue(dto);
                entityProp.SetValue(entity, value);
            }
        }
    }

    public static void MapToDto<TEntity, TDto>(TEntity entity, TDto dto)
    {
        var dtoProps = typeof(TDto).GetProperties();
        var entityProps = typeof(TEntity).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var entityProp = entityProps.FirstOrDefault(p =>
                p.Name == dtoProp.Name &&
                p.PropertyType == dtoProp.PropertyType &&
                dtoProp.CanWrite);

            if (entityProp != null)
            {
                var value = entityProp.GetValue(entity);
                dtoProp.SetValue(dto, value);
            }
        }
    }
}
