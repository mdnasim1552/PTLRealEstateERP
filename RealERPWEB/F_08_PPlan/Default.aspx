

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.Default" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" Width="100%" DataStartField="startdate"
                                            DataEndField="enddate" DataTextField="name" DataIdField="id" DataResourceField="resource_id"
                                            CellGroupBy="Month" Scale="Day" EventMoveHandling="CallBack" OnEventMove="DayPilotScheduler1_EventMove">
                                        </DayPilot:DayPilotScheduler>
    </div>
    </form>
</body>
</html>

</html>
