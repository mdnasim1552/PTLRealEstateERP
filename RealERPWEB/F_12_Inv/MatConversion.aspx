<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MatConversion.aspx.cs" Inherits="RealERPWEB.F_12_Inv.MatConversion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.keynavigation.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

           <%--     var gvisu = $('#<%=this.grvacc.ClientID %>');
                $.keynavigation(gvisu);--%>
                //gvisu.Scrollable();

            });



            $('.chzn-select').chosen({ search_contains: true });



        }

    </script>


   <asp:Updatepanel id="updatepanel1" runat="server">
       
           <Contenttemplate>
             <div class="realprogressbar">
                   <asp:updateprogress id="updateprogress2" runat="server" associatedupdatepanelid="updatepanel1" displayafter="30">
                    <progresstemplate>
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
                    </progresstemplate>
                  </asp:updateprogress>
               </div>
               <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-7 pading5px">
                                        <asp:Label ID="lblpage" runat="server" CssClass="lblTxt lblName">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to"> Date</asp:Label>
                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">ID</asp:Label>
                                        <asp:Label ID="lblCurTransNo1" runat="server" CssClass="inputTxt inputName" Width="60"></asp:Label>
                                        <asp:Label ID="txtCurTransNo2" runat="server" CssClass="inputTxt inputName" Width="60" Style="margin-left: 5px;"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" CssClass="smLbl" Width="80">Conversion No:</asp:Label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectFromList" runat="server" CssClass="lblTxt lblName">Project</asp:Label>
                                         <asp:TextBox ID="txtSrcNotify" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindNotify" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlprjlistfrom" runat="server" style="width:415px;" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                    </div>
                                      <div class="col-md-4 pading5px  pull-right" >
                                            <asp:Label ID="lblprious" runat="server" CssClass=" smLbl_to" Text="Pre List"></asp:Label>
                                            <asp:TextBox ID="txtSrchConvrNo" runat="server" TabIndex="7" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindMCno" runat="server" CssClass="btn btn-primary srearchBtn" Style="float: left;" TabIndex="10" OnClick="ImgbtnFindMCno_Click1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                           <asp:DropDownList ID="ddlPrevMCList" runat="server" CssClass="chzn-select  ddlPage"  TabIndex="6" Width="180px">
                                        </asp:DropDownList>

                                    
                                           <%-- <asp:ListBox ID="ddlPrevReqList" runat="server" Height="100" CssClass="smDropDown";"></asp:ListBox>--%>

                                        </div>

                                </div>
 
                                </div>


                           
                        </fieldset>

                        <asp:Panel ID="pnlgrd" runat="server" Visible="False">
                            <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-7 pading5px asitCol3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName">Material From :</asp:Label>
                                            <asp:TextBox ID="txtSearchRes" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>                                           
                                            <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                         
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlreslistfrom" runat="server" CssClass="chzn-select form-control inputTxt" style="width:336px;" AutoPostBack="true" OnSelectedIndexChanged="ddlreslistfrom_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                                                       
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3"  Visible="false">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="ddlPage62">
                                            </asp:DropDownList>
                                           
                                        </div>
                                         <div class="col-md-1">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselect_Click">Select</asp:LinkButton>

                                        </div>

                                    </div>

<%--                                      <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Material To:</asp:Label>
                                            <asp:TextBox ID="txtSearchRes2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindRes2" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindRes2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlreslistto" runat="server" CssClass="chzn-select form-control inputTxt" style="width:336px;" AutoPostBack="true" OnSelectedIndexChanged="ddlreslistto_SelectedIndexChanged" >
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3" Visible="false">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlResSpcf2" runat="server" CssClass="ddlPage62">
                                            </asp:DropDownList>
                                           
                                        </div>
                                       <div class="col-md-1">
                                            <asp:LinkButton ID="lnkconvert" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkconvert_Click">Convert</asp:LinkButton>

                                     <asp:Label ID="lblConversionNo" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                    </div>--%>








                                </div>


                            </fieldset>
                            </div>


                              <asp:GridView ID="grvconversion" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px" OnRowCreated="grvconversion_RowCreated"
                                OnPageIndexChanging="grvconversion_PageIndexChanging"
                                OnRowDeleting="grvconversion_RowDeleting">
                               <%-- <RowStyle />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                      
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                   <%-- <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                  

<%--                                    <asp:TemplateField HeaderText="Project">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>--%>

                                      <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConvdesc" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfunit" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Specification" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgfspcfdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'  Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="lgvfspcfcod"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgfspcfcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'  Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Balance Qty" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgfbalqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtfqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                             <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Quantity Input</asp:LinkButton>
                                           
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtfrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>

                                        <asp:TextBox ID="txtfamt" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <%--  <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tsirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltunit" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tsirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdateconversion" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger primaryBtn" OnClick="lnkupdateConversion_Click">Update</asp:LinkButton>
                                           
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                      <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvTAmount" runat="server" Style="text-align: right"
                                                Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>

                                             <asp:TextBox ID="txttamt" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>




                                 
                                   <%--   <asp:TemplateField HeaderText="tspcfcode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgtspcfcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tspcfcod")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                       <asp:TemplateField HeaderText="fsircode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgfsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="trsircode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgtsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>




                                  <%--  <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>--%>
                                  <%--  <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Total</asp:LinkButton>
                                           
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />

                                          </asp:TemplateField>--%>

                               

                                 
                                

                                      

                                     

                                
                                 <%--   <asp:TemplateField HeaderText="Converted Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                               
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <br />
                           



                          
                              </div>
                        </asp:Panel>

                    </div>

                     <asp:Panel ID="pnlProduct" runat="server"  Visible="false">
                      <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                  
                                         <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Material To:</asp:Label>
                                            <asp:TextBox ID="txtSearchRes2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindRes2" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindRes2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlreslistto" runat="server" CssClass="chzn-select form-control inputTxt" style="width:336px;" AutoPostBack="true" OnSelectedIndexChanged="ddlreslistto_SelectedIndexChanged" >
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3" Visible="false">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlResSpcf2" runat="server" CssClass="ddlPage62">
                                            </asp:DropDownList>
                                           
                                        </div>
                                       <div class="col-md-1">
                                            <asp:LinkButton ID="lnkconvert" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkconvert_Click">Select</asp:LinkButton>

                                        </div>



                                    </div>
                                


                                   
                                </div>
                            </fieldset>
                          </div>

                            <asp:GridView ID="gvProduct" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                OnPageIndexChanging="gvProduct_PageIndexChanging" OnRowDeleting="gvProduct_RowDeleting" >
                               <%-- <RowStyle />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                      
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                   <%-- <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                     <asp:TemplateField HeaderText="ProjectCode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrpcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                

                                      <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltunit" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                             <asp:LinkButton ID="lnktotalproduct" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary primaryBtn" OnClick="lnktotalproduct_Click">Total</asp:LinkButton>
                                           
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />


                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdateconversion" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger primaryBtn" OnClick="lnkupdateConversion_Click">Convert</asp:LinkButton>
                                           
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                      <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvTAmount" runat="server" Style="text-align: right"
                                                Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>

                                             <asp:TextBox ID="txttamt" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>




                                  
                                      <asp:TemplateField HeaderText="tspcfcode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgtspcfcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="trsircode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgtsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>




                                  <%--  <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>--%>
                                  <%--  <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Total</asp:LinkButton>
                                           
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />

                                          </asp:TemplateField>--%>

                               

                                 
                                     
                                      

                                     

                                
                                 <%--   <asp:TemplateField HeaderText="Converted Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                               
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                           <div class="row">

                              <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                      <div class="form-group">
                                          <div class="col-md-6 pading5px pull-left">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblConvrNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtConvrNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                            </div>


                                        </div>

                                    </div>


                                    </div>
                                  </fieldset>
                               </div>

                         </asp:Panel>



                </div>
            </div>



           </Contenttemplate>
     </asp:Updatepanel>


</asp:Content>

