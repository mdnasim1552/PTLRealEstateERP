<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CustChDishoner.aspx.cs" Inherits="RealERPWEB.F_23_CR.CustChDishoner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/KeyPress.js"></script>


    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



        }

    </script>
    <style>   
        .chzn-container-single .chzn-single {
            height: 29px !important;
            line-height: 29px !important;
        }

        .mt20 {
            margin-top:20px;
        }
    </style>






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
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblchequeno" runat="server" CssClass="lblTxt lblName" Text="Cheque No"></asp:Label>
                                <asp:LinkButton ID="ibtnFindChequeno" CssClass="srearchBtn" runat="server" OnClick="ibtnFindChequeno_Click" TabIndex="1"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtsrchChequeno" runat="server" CssClass="form-control form-control-sm" Visible="false" TabIndex="4"></asp:TextBox>
                                <asp:DropDownList ID="ddlChequeNo" runat="server" CssClass="form-control form-control-sm chzn-select"  Style="width: 330px;" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnSelectChequeNo" runat="server" CssClass="btn btn-primary btn-sm mt20"
                                OnClick="lbtnSelectChequeNo_Click"
                                TabIndex="8">Select</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvchdishoner" runat="server" AutoGenerateColumns="False"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="543px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" ForeColor="Black"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvproject" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Unit & Customer">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvuandcname" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitacname")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Mrr No">
                                <%--<FooterTemplate>
                                    <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"  OnClick="lFinalUpdate_Click"> Update</asp:LinkButton>
                                </FooterTemplate>--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMrrno" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvChequeNo" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDueDate" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvamount" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dishoner Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvDate" runat="server" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dishdate")) %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtgvDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcustphn" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custphn")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
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

