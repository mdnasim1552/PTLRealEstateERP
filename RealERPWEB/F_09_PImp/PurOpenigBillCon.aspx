
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurOpenigBillCon.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurOpenigBillCon" %>

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

            $('.chzn-select').chosen({
                search_contains: true,
              
            });
        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:TextBox ID="txtSearchPro" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select" Width="350px">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px ">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>


                                    <div class="col-md-3 pading5px ">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>



                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSupplier" runat="server" CssClass="lblTxt lblName" Text="Contractor"></asp:Label>
                                        <asp:TextBox ID="txtsrchSupplier" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnFindSupplier" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnFindSupplier_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="chzn-select" Width="350px">
                                        </asp:DropDownList>

                                    </div>





                                </div>

                            </div>
                        </fieldset>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">


                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <asp:Panel ID="pnlgenMrr" runat="server" Visible="false">
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px ">
                                                    <asp:Label ID="lblnoofmrr" runat="server" CssClass="lblTxt lblName" Text="No Of Bill"></asp:Label>
                                                    <asp:TextBox ID="txtnofomrr" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                                 





                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnGenerate_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                </div>





                                            </div>

                                        </asp:Panel>
                                        <div class="form-group">

                                            <div class="col-md-offset-1 col-md-3 pading5px asitCol3">

                                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Generate Bill" CssClass="btn btn-primary checkBox" Style="margin-left: 13px" />

                                            </div>



                                        </div>

                                    </div>

                                </fieldset>
                                <asp:GridView ID="gvMoney" runat="server" AutoGenerateColumns="False"
                                    OnRowDeleting="gvMoney_RowDeleting" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Bill #" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="billno #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvbillno01" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Ref.">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvrefid" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbTotal" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbTotal_Click"> Total </asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MR No">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvmrno" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Chalan No">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvchalanno" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Bill Date">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdateMoney" runat="server" CssClass="btn btn-danger primarygrdBtn" OnClick="lbtnUpdateMoney_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvbilldate" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate"))%>'
                                                    Width="70px"></asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvbilldate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvbilldate">
                                                </cc1:CalendarExtender>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="Bill Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFBillamt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBillamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Remarks">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvrmrks" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent" TextMode="MultiLine"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="200px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>


                            </asp:View>
                        </asp:MultiView>



                    </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
