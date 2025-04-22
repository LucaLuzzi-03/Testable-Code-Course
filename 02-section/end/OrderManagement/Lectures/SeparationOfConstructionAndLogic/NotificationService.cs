namespace OrderManagement.Lectures.SeparationOfConstructionAndLogic;

public class NotificationFormat()
{
    public bool Html { get; private set; }
    public bool IncludingTrakingLink { get; private set; }

    private NotificationFormat(bool useHtml) : this()
    {
        Html = useHtml;
    }

    public static NotificationFormat Create(string serverType)
    {
        return new NotificationFormat(serverType == "Exchange");
    }

    public void WithTrakingLink()
    {
        IncludingTrakingLink = true;
    }
}

public class NotificationService
{
    public async Task SendOrderNotification(string customerEmail, NotificationFormat format)
    {
        // Send notification
    }
}
