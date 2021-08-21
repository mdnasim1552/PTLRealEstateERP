<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCustomerBill.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptCustomerBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {



            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            $('.chzn-select').chosen({ search_contains: true });

            //  $.keynavigation(gridview);
        }
    </script>

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
            <br />

            <div class=" card card-fluid ">
                <div class=" card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="row">
                                <asp:Label ID="lblBillNo" runat="server" class="control-label" Text="Bill No"></asp:Label>
                                <div class="form-group">
                                    <div class="row">
                                        <asp:TextBox ID="txtBillNo1" runat="server" class="col-md-4 form-control" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox ID="txtBillNo2" runat="server" class="col-md-6 form-control" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox ID="txtBillNo" runat="server" Visible="False" class="form-control"></asp:TextBox>
                                    </div>

                                </div>

                            </div>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblDate" runat="server" class="control-label" Text="Date:"></asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblRefno" runat="server" class="control-label" Text="Ref No"></asp:Label>
                            <asp:TextBox ID="txtRefNo" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-2">

                        </div>--%>
                        <div class="col-md-2">

                            <%--<asp:Label ID="Label2" runat="server" class="control-label" Text="Prev BillNo" Style="padding-left:10px"></asp:Label>--%>
                            <asp:LinkButton ID="ltbnPrevBill" runat="server" CssClass="control-label" OnClick="ltbnPrevBill_Click" Style="float: left">Prev Bill</asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevBillNo" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlPrevBillNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                        </div>

                        <%--  <div class="col-md-12">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblBillNo" runat="server" class="col-md-4" Text="Bill No"></asp:Label>
                                            <asp:TextBox ID="txtBillNo" runat="server" class="col-md-8"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblDate" runat="server" class="col-md-4" Text="Date:"></asp:Label>
                                            <asp:TextBox ID="txtDate" runat="server" class="col-md-8" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblRefNo" runat="server" class="col-md-4" Text="Ref No"></asp:Label>
                                            <asp:TextBox ID="txtRefNo" runat="server" class="col-md-8"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label" for="project" id="lblproject" runat="server">Project Name</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="true" CssClass=" form-control chzn-select" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProjectName" runat="server" Visible="False" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label" for="Customer" id="lblCustName" runat="server">Scope of Work</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass=" form-control chzn-select"></asp:DropDownList>
                                        <asp:Label ID="lblddlCustName" runat="server" Visible="False" CssClass="form-control"></asp:Label>
                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass=" btn btn-primary  btn-sm" OnClick="lbtnOk_Click" Style="margin-left: 20px;">Ok</asp:LinkButton>--%>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary  btn-sm" OnClick="lbtnOk_Click" Style="margin-left: 20px; float: left">Ok</asp:LinkButton>
                                        <%--<asp:LinkButton ID="lbtnPrevOk" runat="server" CssClass=" btn btn-primary  btn-sm" OnClick="lbtnPrevOk_Click" Visible="false" Style="margin-left: 20px; float: left">Ok</asp:LinkButton>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label" for="subject" id="Label1" runat="server">Subject : </label>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSubject" CssClass="col-md-12" runat="server" TextMode="MultiLine" Rows="2">Running bill for architectural design and drawing of ..</asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label" id="lblsubject" runat="server">Dear Sir, </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtLetter" CssClass="col-md-12" runat="server" TextMode="MultiLine" Rows="3">It Is to inform you that, as per our offer agreement we have provided the following services for your project ..</asp:TextBox>
                                        <%--<label class="control-label" id="txtHeader" runat="server" >It Is to inform you that, as per our offer agreement we have provided the following services fro your project 'Finlay Idrees Chalet storied (with one basement) aparment complex at plot #91 College Road, Chakbazar, Chattogram'</label>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="form-group">
                                        <label class="control-label" id="Label2" runat="server">Thanking You, </label>
                                        <asp:TextBox ID="txtThank" class="col-md-2 form-control" runat="server">Mr.</asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            </div>



            <div class=" card card-fluid">
                <div class=" card-body" style="min-height: 250px;">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvCustBill" runat="server" AllowPaging="false"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCustBill_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblslno" runat="server" Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdate" runat="server"
                                                OnClick="lnkbtnUpdate_Click" CssClass="btn  btn-primary btn-sm">Update</asp:LinkButton>
                                        </FooterTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkgvHeader" runat="server" Font-Bold="True" ToolTip="Edit Header"><i class="fa fa-th-large" aria-hidden="true"></i></asp:LinkButton>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="True" ToolTip="Delete" Style="text-align: center" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click">

                                                        <i class=" fa fa-trash" style="align-items:center"></i>

                                            </asp:LinkButton>

                                        </ItemTemplate>
                                        

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Items Description">
                                        <FooterTemplate>
                                            <table style="width: 10%; height: 48px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblgv01" runat="server" Font-Bold="True" Text="Total"
                                                            Font-Size="12px" Style="text-align: right" Width="400px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblgv02" runat="server" Font-Bold="True" Font-Size="12px" Text="Received"
                                                            Style="text-align: right" Width="400px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblgv03" runat="server" Font-Bold="True" Font-Size="12px" Text="Net Payable"
                                                            Style="text-align: right" Width="400px"></asp:Label>
                                                    </td>
                                                </tr>


                                            </table>

                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemdesc" runat="server" Style="text-align: left; margin-left: 10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="400px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount (TK) ">
                                        <FooterTemplate>
                                            <table style="width: 10%; height: 48px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblgvScamt" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Style="text-align: right" Width="100px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblvalRecv" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="100px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblvalNetPay" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="100px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnUpdate_Click" Text="Update" Style="float: right" Width="100px"></asp:LinkButton>

                                                    </td>
                                                </tr>--%>
                                            </table>

                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSchamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>

            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

