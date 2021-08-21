<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPayLandOwner.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPayLandOwner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-8">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BackColor="#CDDBC8" CssClass="rbtnList1" Style="margin-left: 159px;"
                                                    RepeatColumns="6" RepeatDirection="Horizontal"
                                                    Width="300px" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                    <asp:ListItem> Payment Schedule </asp:ListItem>
                                                    <asp:ListItem>Payment</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="10"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            </div>
                                            <div class="col-md-3 pull-right">
                                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>


                                            </div>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblSearch" CssClass="lblTxt lblName" runat="server" Text="Land Owner"></asp:Label>
                                            <asp:TextBox ID="txtsrchland" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnsrchland" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnsrchland_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:DropDownList ID="ddlLand" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblandown" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>

                                        </div>



                                    </div>
                                </fieldset>
                         
                  

                           

                           <asp:Multiview ID="MultiView" runat="server">
                                   <asp:View ID="view" runat="server">

                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lperInfo" runat="server" CssClass="btn btn-success primaryBtn" Text="Personal Information"></asp:Label>
                                            <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                            <asp:LinkButton ID="lbtnBack" runat="server"
                                                OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>
                                            <div class="clearfix"></div>
                                        </div>



                                    </div>






                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="831px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px" ForeColor="Black" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="510px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Height="20px" Font-Size="11px"></asp:TextBox>
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
                                    <div class="form-group">
                                        <asp:Label ID="lperInfo0" runat="server" CssClass="btn btn-success primaryBtn" Text="Payment Schedule"></asp:Label>
                                        <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                        <div class="clearfix"></div>

                                    </div>


                                    <asp:Panel ID="Panel3" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">First Ins. Date</asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label5" runat="server" Font-Size="11px" CssClass="lblTxt lblName">Total Installement</asp:Label>
                                            <asp:TextBox ID="txtTInstall" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                            <asp:Label ID="bleTotal" runat="server" Font-Size="11px" CssClass="lblTxt lblName">Total </asp:Label>
                                            <asp:TextBox ID="txtToatal" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Duration</asp:Label>
                                            <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True"
                                                CssClass="ddlPage" Width="120px">
                                                <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                <asp:ListItem Value="7">7  Month</asp:ListItem>
                                                <asp:ListItem Value="8">8  Month</asp:ListItem>
                                                <asp:ListItem Value="9">9  Month</asp:ListItem>
                                                <asp:ListItem Value="10">10  Month</asp:ListItem>
                                                <asp:ListItem Value="11">11  Month</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtnGenerate" runat="server" OnClick="lbtnGenerate_Click" CssClass="btn btn-primary primaryBtn">Generate</asp:LinkButton>
                                            <div class="clearfix"></div>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="PanelAddIns" runat="server" Visible="False">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Installement</asp:Label>
                                                <asp:TextBox ID="txtsrchInstallment" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtnFindInstallment" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindInstallment_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:LinkButton ID="lbtnAddInstallment" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>

                                        </div>
                                    </asp:Panel>


                                    <asp:Panel ID="pnlSlab" runat="server" Visible="False">
                                        <div class="form-group">
                                            <div class="col-md-8 pading5px">
                                                <asp:Label ID="lblfrmslab" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                <asp:TextBox ID="txtfrmslab" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                <asp:Label ID="lbltoslab" runat="server" CssClass="lblTxt lblName">To</asp:Label>
                                                <asp:TextBox ID="txttoslab" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                <asp:Label ID="lblinsamt" runat="server" CssClass="lblTxt lblName">Installment </asp:Label>
                                                <asp:TextBox ID="txtperslabamt" runat="server" CssClass="inputTxt inpPixedWidth" Style="text-align: right;"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnSlab" runat="server" CssClass="btn btn-primary primaryBtn margin5px" OnClick="lbtnSlab_Click">Put Data</asp:LinkButton>
                                            </div>


                                            <div class="clearfix"></div>

                                        </div>
                                    </asp:Panel>




                                    <div class="form-group">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <div class="form-group">
                                                <asp:Label ID="lPays" runat="server" CssClass="lblTxt lblName">Payment Shedule</asp:Label>
                                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment" />
                                                <asp:CheckBox ID="chkSegment" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkSegment_CheckedChanged" Text="Slab" />

                                                <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment" />
                                            </div>



                                        </asp:Panel>
                                    </div>
                                </fieldset>

                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="223px" OnRowDeleting="gvPayment_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatpayment" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatpayment_Click">Update Payment Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lTotalPayment" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                               </asp:View>

                               <asp:view id="view1" runat="server">

                                 
                                   </asp:view>
                                 </asp:Multiview >
                       
                    </div>


                </div>          

            </div>


          
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





