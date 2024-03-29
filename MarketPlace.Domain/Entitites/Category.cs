﻿using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Category : AuditableEntity
{
    public long Id { get; set; }
    public long? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public long ImageId { get; set; }
    public Image Image { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public virtual ICollection<Product>? Products { get; set; }
    public virtual ICollection<Category>? ChildrenCategories { get; set; }
    public virtual ICollection<SpecificationType>? SpecificationTypes { get; set; }
}
