<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustChDishoner.aspx.cs" Inherits="RealERPWEB.F_23_CR.CustChDishoner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/KeyPress.js"></script>


    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



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

            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblchequeno" runat="server" CssClass="smLbl_to" Text="Cheque No"></asp:Label>
                                        <asp:TextBox ID="txtsrchChequeno" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindChequeno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindChequeno_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:DropDownList ID="ddlChequeNo" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnSelectChequeNo" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnSelectChequeNo_Click"
                                            TabIndex="8">Select</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"> </asp:Label>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                    </div>
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
                                        Width="130px"></asp:Label>
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
                                <FooterTemplate>
                                    <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"  OnClick="lFinalUpdate_Click"> Update</asp:LinkButton>
                                </FooterTemplate>
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


            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

