using System.Windows;

namespace CLBuilder.view
{
    /// <summary>
    /// Interaction logic for TextView.xaml
    /// </summary>
    public partial class TextView : Window
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The text property
        /// </summary>
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(TextView), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(ItemNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The item name property
        /// </summary>
        public static readonly DependencyProperty ItemNameProperty =
        DependencyProperty.Register("ItemName", typeof(string), typeof(TextView), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The description property
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(string), typeof(TextView), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Initializes a new instance of the <see cref="TextView"/> class.
        /// </summary>
        public TextView()
        {
            InitializeComponent();
        }
    }
}