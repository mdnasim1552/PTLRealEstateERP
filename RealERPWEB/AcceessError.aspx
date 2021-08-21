<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AcceessError.aspx.cs" Inherits="RealERPWEB.AcceessError" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    background-color: blue;
}
100% {
    background-color: red;
}
}
@keyframes color_change {
0% {
    background-color: blue;
}
100% {
    background-color: red;
}
}

</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row" style="margin-top:100px;">

                <div class="col-md-7 col-md-offset-2">
            <div class="panel panel-primary">
                <div class="panel-heading btnRegView" style=" background:#ff0000;" >
                    <h4 class="panel-title">
                       <asp:Label ID="lblAccessError" runat="server" style="text-align:center;" 
        Text="Required Authentication Not Found. Access Is Denied"></asp:Label>

                    </h4>
                    
                </div>
                <div class="panel-body">
                   Access Denied. Contact your administrator, or please send your Query, <a href="mailto:info@pintechltd.bd">info@pintechltd.com</a></div>
            </div>
        </div>

                 
            </div>
        </div>
    </div>

    


</asp:Content>


