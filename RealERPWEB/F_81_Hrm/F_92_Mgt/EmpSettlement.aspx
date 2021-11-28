<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpSettlement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpSettlement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });
            $('.chzn-select').chosen({ search_contains: true });

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
                                    <div class="col-md-3 pading5px  ">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Ref No.</asp:Label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to"> Date</asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                        </cc1:CalendarExtender>
                                     
                                    </div>
                                      <div class="col-md-1 pading5px ">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee List</asp:Label>
                                     </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblEmpname" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-3 pull-right">
                                         <div class="col-md-12">

                                        <asp:RadioButtonList ID="rbtnstatement" runat="server" AutoPostBack="True" Visible="false"
                                            BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px"
                                            Font-Bold="True" Font-Size="11px" ForeColor="Black" OnSelectedIndexChanged="rbtnstatement_OnSelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">English</asp:ListItem>
                                            <%--<asp:ListItem>বাংলা</asp:ListItem>--%>
                                        </asp:RadioButtonList>

                                    </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    <asp:Panel ID="ViewDataPanel" runat="server" Visible="false">
                        <div class="form-horizontal">
                            <div class="col-md-4" runat="server" id="engst" Visible="True">
                                <table class="table table-bordered table-responsive table-condensed table-hover">
                                    <tr class="bg-success">
                                        <td>Name</td>
                                        <td>
                                            <asp:Label ID="lblname" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>Designation</td>
                                        <td>
                                            <asp:Label ID="lbldesig" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                      <tr class="bg-success">
                                        <td>Id Card No</td>
                                        <td>
                                            <asp:Label ID="lblidcard" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>Section/Department</td>
                                        <td>
                                            <asp:Label ID="lblsection" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr class="bg-success">
                                        <td>Job Seperation Type</td>
                                        <td>
                                            <asp:Label ID="lblseptype" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>Joining Date</td>
                                        <td>
                                            <asp:Label ID="lbljoin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr class="bg-success">
                                        <td>Seperation Date</td>
                                        <td>
                                            <asp:Label ID="lblsep" runat="server"></asp:Label>
                                        </td>
                                          <tr>
                                        <td>Service Length</td>
                                        <td>
                                            <asp:Label ID="lblservlen" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    </tr>
                                </table>
                            </div>
                            
                            <div class="col-md-4" runat="server" id="bngst" Visible="false">
                                <table class="table table-bordered table-responsive table-condensed table-hover">
                                    <tr>
                                        <td>তারিখ</td>
                                        <td>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="bg-success">
                                        <td>নাম</td>
                                        <td>
                                            <asp:Label ID="lblnam" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>পদবী</td>
                                        <td>
                                            <asp:Label ID="lbldesg" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                      <tr class="bg-success">
                                        <td>আই ডি নং</td>
                                        <td>
                                            <asp:Label ID="lblid" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>সেকশন/বিভাগ</td>
                                        <td>
                                            <asp:Label ID="lblsec" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr class="bg-success">
                                        <td>চাকুরী পৃথকীকরনের ধরন </td>
                                        <td>
                                            <asp:Label ID="lbljobtype" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>চাকুরীতে যোগদানের তারিখ</td>
                                        <td>
                                            <asp:Label ID="lbljdate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr class="bg-success">
                                        <td>পৃথকীকরনের তারিখ</td>
                                        <td>
                                            <asp:Label ID="lblsepdate" runat="server"></asp:Label>
                                        </td>
                                          <tr>
                                        <td>চাকুরীর মেয়াদকাল</td>
                                        <td>
                                            <asp:Label ID="lbljonlen" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    </tr>
                                </table>
                            </div>
                            

                            <div class="col-md-8">
                                <span class="label label-success"><big>Salary Information</big></span>
                                <asp:GridView ID="gvsettlemntcredit" OnRowDataBound="gvsettlemntcredit_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                      
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit Information">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcreditinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                     <asp:Label ID="lblhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Amount">
                                                
                                                <ItemTemplate>
                                                     <asp:Label ID="lblamount" style="text-align:right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                              
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day/Hour">
                                                
                                                <ItemTemplate>
                                                     <asp:TextBox ID="txtnumofday" style="text-align:right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                              
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount (Day/Hour)">
                                               
                                                <ItemTemplate>
                                                     <asp:TextBox ID="txtperday" style="text-align:right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblftotal" runat="server">Total Amount</asp:Label>
                                                </FooterTemplate> 
                                                 <FooterStyle HorizontalAlign="Right" Font-Bold="true" />                                               
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount">
                                             
                                                <ItemTemplate>
                                                  <asp:TextBox ID="TtlAmout" runat="server" style="text-align:right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfttlamt" runat="server" style="text-align:right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                    </asp:GridView>
                                <span class="label label-success "><big>Deduction Information</big></span>
                                
                                  <asp:GridView ID="gvsttlededuct" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                      
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deduct Information">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcreditinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                        Width="250px"></asp:Label>
                                                       <asp:Label ID="lblhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Day/Hour">
                                                
                                                <ItemTemplate>
                                                     <asp:TextBox ID="txtnumofday" style="text-align:right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                              
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount (Day/Hour)">
                                               
                                                <ItemTemplate>
                                                     <asp:TextBox ID="txtperday" style="text-align:right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                              
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblftotal" runat="server">Total Deduction Amount</asp:Label>
                                                </FooterTemplate>    
                                                 <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount">
                                             
                                                <ItemTemplate>
                                                  <asp:TextBox ID="TtlAmout" style="text-align:right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                </ItemTemplate> 
                                                  <FooterTemplate>
                                                    <asp:Label ID="lblgvfdedttlamt" runat="server" style="text-align:right"></asp:Label>
                                                </FooterTemplate>     
                                                 <FooterStyle HorizontalAlign="Right" Font-Bold="true" />                                        
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                    </asp:GridView>


                                  <table class="table-striped table-hover table-bordered" >
                                      <tr class="bg-danger">
                                          <td class="text-right" style="width:580px; color:black; font-weight:bolder; font-size:13px;">Net Payable Amount</td>
                                           <td style="width:130px" class="text-right">
                                            <asp:Label ID="NetAmount" runat="server" Font-Bold="true" style=" color:black; font-weight:bolder; font-size:13px;"></asp:Label>
                                           </td>
                                      </tr>
                                  </table>  
                            </div>
                          
                        </div>
                    </asp:Panel>



                   
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


