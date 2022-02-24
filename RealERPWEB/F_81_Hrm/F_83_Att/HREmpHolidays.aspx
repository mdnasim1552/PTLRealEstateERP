<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HREmpHolidays.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HREmpHolidays" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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


        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="card card-fluid container-data mt-5" id='printarea'>
        <div class="card-body" style="min-height: 600px;">
            <div class="row">
                <div class="col-md-3 p-0">
                <div class="input-group input-group-alt">
                    <div class="input-group-prepend">
                        <button class="btn btn-secondary" type="button">Month</button>
                    </div>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                    </asp:DropDownList>

                </div>
            </div>
            </div>
            <div class="row mt-4">
                <div class="form-group">
                
                        <asp:CheckBoxList ID="chkDate" runat="server" CssClass="btn checkBox"
                            RepeatColumns="7" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>

                        





                </div>
            </div>
            
            
        </div>
    </div>
</asp:Content>
