<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ErrorHandling.aspx.cs" Inherits="RealERPWEB.ErrorHandling" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .alert-message
{
    margin: 20px 0;
    padding: 20px;
    border-left: 3px solid #eee;
}
.alert-message h4
{
    margin-top: 0;
    margin-bottom: 5px;
}
.alert-message p:last-child
{
    margin-bottom: 0;

}
.alert-message code
{
    background-color: #fff;
    border-radius: 3px;
}
.alert-message-success
{
    background-color: #F4FDF0;
    font-size:16px;
    border-color: #3C763D;
}
.alert-message-success h4
{
    color: #3C763D;
     font-size:20px;
}
.alert-message-danger
{
    background-color: #fdf7f7;
    border-color: #d9534f;
}
.alert-message-danger h4
{
    color: #d9534f;
}
.alert-message-warning
{
    background-color: #fcf8f2;
    border-color: #f0ad4e;
}
.alert-message-warning h4
{
    color: #f0ad4e;
}
.alert-message-info
{
    background-color: #f4f8fa;
    border-color: #5bc0de;
}
.alert-message-info h4
{
    color: #5bc0de;
}
.alert-message-default
{
    background-color: #EEE;
    border-color: #B4B4B4;
}
.alert-message-default h4
{
    color: #000;
}
.alert-message-notice
{
    background-color: #FCFCDD;
    border-color: #BDBD89;
}
.alert-message-notice h4
{
    color: #444;
}
    </style>
    <asp:Label ID="lblSssion" runat="server" Visible="false">
<div class="col-sm-6 col-md-6 col-md-offset-3">
         <div class="alert-message alert-message-success">
                <h4>
                   For Your Attention!!</h4>
                <p>
                  Your Login Session May be Timeout. Please <asp:HyperLink  ID="LognHypl" runat="server" NavigateUrl="~/LogIn.aspx"
                      >Login</asp:HyperLink> Again <strong> <br />
                      For Contact Administration <br />
                                                                                                                                       </strong> <a href="https://pintechltd.com/">
                           Pinovation Tech Ltd </a>
                   
           <br />
Center Point (8th floor), Unit-F,    <br />
14/A, Tejkunipara, Tejgaon, FarmGate,     <br />
Dhaka - 1215. Bangladesh     <br />
P: (+8802) 9116439,9118665, 9143472, 9033408,    <br />
Shovon +880-1796-999992    <br />

Mostak +880-1717-545613    <br />

www.pintechltd.com   <br />

Email: info@pintechltd.com
                       <img src="Images/asitbanner.png"  width="100%"/>
                </p>

            </div>
        </div>


    </asp:Label>
     <asp:Label ID="lblDbError" runat="server" Visible="false">
         <div class="col-sm-6 col-md-6 col-md-offset-3">
            <div class="alert-message alert-message-success">
                <h4>
                   For Your Attention!!</h4>
                <p>
                  Your Database Connection May Be Loss or Something Went Wrong. Please Ensure Database Connection to Solve This Problem<strong>
                      Contact Administration <br />
                                                                                                                                       </strong> <a href="http://pintechltd.com/">
                           Pinovation Tech Ltd </a>
                   
           <br />
Center Point (8th floor), Unit-F,    <br />
14/A, Tejkunipara, Tejgaon, FarmGate,     <br />
Dhaka - 1215. Bangladesh     <br />
P: (+8802) 9116439,9118665, 9143472, 9033408,    <br />


www.pintechltd.com   <br />

Email: info@pintechltd.com
                       <img src="Images/asitbanner.png"  width="100%"/>
                </p>

            </div>
        </div>
     </asp:Label>
</asp:Content>

