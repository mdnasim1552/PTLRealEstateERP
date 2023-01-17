<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MatTransStatus.aspx.cs" Inherits="RealERPWEB.F_12_Inv.MatTransStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


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
            <script type="text/javascript" language="javascript">

                $(document).ready(function () {

                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

                });

                function pageLoaded() {


                    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

                }

            </script>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
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
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblProjectFromList" runat="server" CssClass="lblTxt lblName">From Date</asp:Label>


                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">To</asp:Label>

                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass=" chzn-select form-control inputTxt" style="width:336px;">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-left:-50px;" >Ok</asp:LinkButton>

                                    </div>

                                
                                

                                </div>

                                <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="toproject" runat="server" CssClass="lblTxt lblName">To Project</asp:Label>
                                    <asp:TextBox ID="txtproject" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="btntoproject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="btntoproject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control  inputTxt" style="width:336px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblddlProjectTo" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                </div>

                                

                            </div>

                                   <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblresName" runat="server" CssClass="lblTxt lblName" Text="Material"></asp:Label>
                                        <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnresource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnresource_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div>
                                         <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black"
                                            MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="345px">
                                        </cc1:DropCheck>
                                 

                                       

                                    </div>

                                   


                                </div>
                         

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl">Ref. No.:</asp:Label>
                                        <asp:TextBox ID="txtSrcRefNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindRefno" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindRefno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <div class="colMdbtn pading5px">
                                            <asp:CheckBox ID="chkProjectTrnsTo" runat="server" Text="Project To" CssClass="btn btn-primary checkBox" />

                                        </div>

                                        
                                 

                                       

                                    </div>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="grvacc_PageIndexChanging"
                        ShowFooter="True" Width="501px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trans.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrnNo" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="MTRF No">
                                <ItemTemplate>
                                    <asp:Label ID="lblrefno" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="MTRF NO/Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblrefno" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Gate pass(Manual)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgaterefno" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server"
                                      
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tdate")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project From">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjFrom" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfproj"))  %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project To">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjTo" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttproj")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material">
                                <ItemTemplate>
                                    <asp:Label ID="lblMat" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server"
                                        Style="font-size: 12px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tunit")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty.">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="55px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbltotalt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Text="Total" Width="55px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right"
                                    VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="55px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right"
                                    VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="85px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="85px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right"
                                    VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Narration">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnarraion" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narration")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
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

