using System;
using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Constructs new instance of list sections.
    /// </summary>
    public class ListSectionBuilder
    {
        private readonly List<IEnumerable<Marker>> _listItems = new List<IEnumerable<Marker>>();
        private readonly List<Attribute> _attributes = new List<Attribute>();
        private string _listType;

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="itemBuilder">Action used to build the list item</param>
        /// <returns>Returns the list section builder instance.</returns>
        public ListSectionBuilder WithListItem(Action<ListItemBuilder> itemBuilder)
        {
            var listItemBuilder = new ListItemBuilder();
            itemBuilder(listItemBuilder);
            
            _listItems.Add(listItemBuilder.Build());
            
            return this;
        }
        
        /// <summary>
        /// Defines a new attribute for the section.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        /// <returns>Returns the list section builder.</returns>
        public ListSectionBuilder WithAttribute(string name, string value)
        {
            _attributes.Add(new Attribute(name,value));
            
            return this;
        }

        /// <summary>
        /// Sets the type of list to render.
        /// </summary>
        /// <param name="listType">List type to render.</param>
        /// <returns>Returns the list section builder instance.</returns>
        public ListSectionBuilder WithListType(string listType)
        {
            _listType = listType;

            return this;
        }
        
        /// <summary>
        /// Builds the new instance of the list section.
        /// </summary>
        /// <returns>Returns the new list section instance.</returns>
        public ListSection Build()
        {
            return new ListSection(_listType, _listItems, _attributes);
        }
    }
}