<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DataBackup.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.DataBackup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>--%>
    <style>
            .btnRegView {
    animation: 1s ease 0s alternate none infinite running color_change;
    color: #ffffff;
    padding: 5px 35px;
    font-size:20px;
}
    .btnRegView h4{
        font-size:25px;
    }
@keyframes color_change {
0% {
    background-color: #DFF0D8;
}
100% {
    background-color: #5CB85C;
}
}
@keyframes color_change {
0% {
    background-color: #155273;
}
100% {
    background-color: #33CD33;
}
}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).keyup(function (event) {
                var key = event.keyCode || event.charCode || 0;
                if (key == 49) { // Page One Press 1
                    eval($("[id$='lbPage1']").attr("href"));
                } else if (key == 50) { // Page Two Press 2
                    eval($("[id$='lbPage2']").attr("href"));
                } else if (key == 51) { // Page Three Press 3                 
                    eval($("[id$='lbPage3']").attr("href"));
                } else if (key == 52) { // Alert Press 4
                    $("[id$='btnBackup']").trigger('click');
                }
            });
        });
    </script>

    <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <div class="col-md-7 col-md-offset-2">
            <div class="panel panel-primary">
                <div class="panel-heading btnRegView" style=" background:#ff0000;" >
                    <h4 class="panel-title">
                       <asp:Label ID="lblDBbackup" runat="server" style="text-align:center;" 
        Text="Backup SQL Server DataBase"></asp:Label>

                    </h4>
                    
                </div>
                <div class="panel-body">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                     <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-warning " Text="Backup DataBase(Alt+4)" OnClick="btnBackup_Click" /></div>
            </div>
        </div>




                
            </div>
        </div>
    </div>
</asp:Content>


