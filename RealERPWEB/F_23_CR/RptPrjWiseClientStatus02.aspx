<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjWiseClientStatus02.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptPrjWiseClientStatus02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
     <script type="text/javascript">
      
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded()
        {
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>

            <%--   var dgvAccRec02 = $('#<%=this.dgvAccRec02.ClientID %>');

            dgvAccRec02.gridviewScroll({
                width: 1160,
                height: 420,             
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });--%>

            $('.chzn-select').chosen({ search_contains: true });

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
                                        <asp:Label ID="lblProjectname" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <asp:Label ID="lblmsg1" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-5  pading5px  ">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to " Text="Size :"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" CssClass="inputTxt ddlPage" TabIndex="13" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                    <%--<div class="col-md-1 pull-left pading5px ">

                                        <asp:CheckBox ID="chkPayDateWise" runat="server" TabIndex="10" Visible="false" Text="Pay Date Wise" CssClass="btn btn-primary checkBox" />

                                    </div>--%>





                                    <div class="col-md-3 pading5px">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar" Text="Please wait......"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                           <%--  <asp:Panel ID="pnlfilter" runat="server">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4 ">
                                        <asp:Label ID="lblAmount0" runat="server" Text="Amount:" CssClass="  lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlSrchCash" runat="server" CssClass="form-control " TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged" Width="209px">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then&nbsp; Equal</asp:ListItem>
                                            <asp:ListItem Value="between">Between</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:TextBox ID="txtAmountC1" runat="server" CssClass="iinputTxt inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="lblToCash" runat="server" CssClass=" smLbl_to blName lblTxt" Text="To:" Visible="false"></asp:Label>

                                        <asp:TextBox ID="txtAmountC2" runat="server" Visible="false" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                    </div>



                                </div>
                                </asp:Panel>--%>

                            </div>

                        </fieldset>

             
                                <div class="table table-responsive">
                                <asp:GridView ID="gvProClientst" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    Width="654px" OnPageIndexChanging="gvProClientst_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unsold Unit No">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFunit" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uunitname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unsold Unit Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFunsold" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunsold" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unusize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Sold Unit No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgsoldunitno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sunitname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sold Unit Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsounitsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsounitsize" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Unit Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtounitsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtounitsize" runat="server" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>






                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgacunamecst" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                       %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Sales Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFaptcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvaptcost" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modification </br> Charge">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmodifi" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmodifi" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "modifee")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Association Fee">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAssofee" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAssofee" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assofee")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Delay Charge">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdelchargecst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdelchargecst" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delayfee")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Registration Cost">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRegis" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRegis" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "regfee")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtRecived" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtRecived" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtdue" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtdue" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todue")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                      
                                        <asp:TemplateField HeaderText="Registration Status">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvregistration" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "register")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

