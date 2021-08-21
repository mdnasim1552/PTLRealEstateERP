<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryMonCharges.aspx.cs" Inherits="RealERPWEB.F_22_Sal.EntryMonCharges" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
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
                                        <asp:Label ID="lblproname" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblscustomer" runat="server" CssClass="lblTxt lblName">Customer Name</asp:Label>
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCustomer_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustomer" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>

                                   

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblMonth" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                        <asp:DropDownList ID="ddlyearmon" runat="server" CssClass=" ddlPage"></asp:DropDownList>



                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:Label ID="lblreference" runat="server" CssClass=" smLbl_to">Reference</asp:Label>
                                        <asp:TextBox ID="txtreference" runat="server" CssClass=" inputtextbox" Style="width: 313px;"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lmsg" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>

                                    </div>




                                </div>
                                <div class="form-group">
                                    <div class="col-md-7 pading5px ">
                                        <asp:Label ID="lblsubject" runat="server" CssClass="lblTxt lblName">Subject</asp:Label>

                                        <asp:TextBox ID="txtSubject" runat="server" CssClass=" inputtextbox" Style="width: 485px;"></asp:TextBox>


                                    </div>

                                </div>
                                <div class="form-group">
                                      <div class="  col-md-7  pading5px ">
                                    <asp:Label ID="lbltype" runat="server" CssClass="lblTxt lblName">Type</asp:Label>
                                  
                                        <asp:RadioButtonList ID="rbtntype" runat="server" CssClass="btn btn-primary  checkBox"
                                            RepeatColumns="6" RepeatDirection="Horizontal" Style="text-align: center; width:485px;"
                                           >
                                            <asp:ListItem Selected="True">Cover Letter</asp:ListItem>
                                            <asp:ListItem>Monthly Charges</asp:ListItem>
                                            <asp:ListItem>Other Charges</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>

                                </div>

                            </div>
                        </fieldset>

                        <asp:GridView ID="gvcharges" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="410px" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgdesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="150px" Height="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Value ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                            Font-Size="12px" OnClick="lbtnUpdate_Click" CssClass="btn  btn-danger primarygrdBtn"> Update </asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdescvalue" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="450px" TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcharges" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->









        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

