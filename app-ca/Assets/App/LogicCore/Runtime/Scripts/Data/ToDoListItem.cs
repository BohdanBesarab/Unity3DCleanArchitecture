using JetBrains.Annotations;

namespace App.LogicCore.Runtime.Scripts.Data
{
    /// <summary>
    /// Represents a valueobject - data with minimal logic in it.
    /// </summary>
    public class ToDoListItem
    {
        private string title;
        private string body;

        public string Title => title;

        public string Body => body;

        public ToDoListItem([NotNull] string title)
        {
            title = title ?? throw new TitleNullException(nameof(title));
        }

        public bool IsValidItem()
        {
            return !string.IsNullOrEmpty(title);
        }
    }
}