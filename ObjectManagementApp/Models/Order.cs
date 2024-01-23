namespace ObjectManagementApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }

        public List<CustomObject> CustomObjects { get; set; } = [];
    }
}
