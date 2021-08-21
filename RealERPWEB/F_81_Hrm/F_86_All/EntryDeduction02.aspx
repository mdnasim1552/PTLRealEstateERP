<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryDeduction02.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_86_All.EntryDeduction02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {



            var gvabscount = $('#<%=this.gvdeduction2.ClientID %>');
                     
            gvabscount.Scrollable();

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

              
                $.keynavigation(gvOvertime);
                $.keynavigation(gvBankPay);
                $.keynavigation(gvEmpHoliday);
                $.keynavigation(gvEmpMbill);
                $.keynavigation(gvEmpELeave);
                $.keynavigation(gvEmpOtherded);
                $.keynavigation(gvEmploan);
                $.keynavigation(gvarrear);
            });
//  gvEmpMbill.Scrollable();



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
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>

                                        <asp:DropDownList ID="ddlyearmon"  runat="server">
                                           <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindDepartment" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindDepartment_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click" Text="Ok"></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="233" CssClass="form-control inputTxt chzn-select" TabIndex="7" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False" Width="233"></asp:Label>
                                    </div>

                                </div>


                                   <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSecSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSection" runat="server"  CssClass="form-control inputTxt chzn-select" TabIndex="6" Width="233">
                                        </asp:DropDownList>

                                      
                                    </div>

                                </div>
                                <div class="form-group" visible="false" >
                                    <asp:Panel ID="pnlpage" runat="server">
                                        <div class="col-md-3 pading5px asitCol3" >
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    </div>

                                    </asp:Panel>

                                        <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl">Code</asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSearchEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                                                                                                                                
                                       </div>
                                   
                                    
                                    
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                     <asp:RadioButtonList ID="rbtnlistsaltype" runat="server" CssClass="rbtnList1 margin5px"
                                                        Font-Size="14px" Height="16px" RepeatColumns="14" RepeatDirection="Horizontal"
                                                        Width="380px" Visible="false">
                                                        <asp:ListItem Selected="True">Management</asp:ListItem>
                                                        <asp:ListItem>Non Management</asp:ListItem>
                                                        <asp:ListItem >Both</asp:ListItem>
                                         </asp:RadioButtonList>

                                </div>
                            </div>
                        </fieldset>
                    </div>


                    

                    <div class="row">
                                                                                                                                        
                            
                            <asp:GridView ID="gvdeduction2" runat="server" AutoGenerateColumns="False" AllowPaging="false" 
                                OnPageIndexChanging="gvdeduction2_PageIndexChanging" ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo66" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                      <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeductiondelete" runat="server" OnClick="btnDeductiondelete_Click" ><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
                                    </ItemTemplate>
                                      </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvEmpId" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Section">
                                        <HeaderTemplate>
                                             <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                  <asp:Label ID="lgvSection" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="200px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

<%--                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnCalAbsCount" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" OnClick="lbtnCalAbsCount_Click"
                                                Style="text-decaration: none;">Calculation</asp:LinkButton>
                                        </FooterTemplate>--%>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                      

                                    <asp:TemplateField HeaderText="Card #">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotaldeduc" runat="server" CssClass="btn btn-primary primaryBtn"  OnClick="lbtnTotaldeduc_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCardno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbntUpdateDec" runat="server" OnClick="lbntUpdateDec_Click" CssClass="btn btn-danger primaryBtn" >Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEmpDesig" runat="server" Height="16px" Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lgvFDesig" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="160px"> Total :</asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Absent</br>Day">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtabsday" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absday")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFabsday" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField> 
                                    
                                    
                                    <asp:TemplateField HeaderText="Leave</br>Approved">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtapprob" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "approved")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFabprob" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>  
                                    
                                     <asp:TemplateField HeaderText="Late Day">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtlateday" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lateday")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlateday" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField> 
                                    
                                     <asp:TemplateField HeaderText="Fine Day">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtfineday" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fineday")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFfineday" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>  
                                    
                                      <asp:TemplateField HeaderText="Deduction">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdeduction" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "deduction")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdeduction" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="adjustment">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtadjustamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjustamt")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFadjustamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     
                                    
                                     <asp:TemplateField HeaderText="Advanced ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtavd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saladv")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFavd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField> 
                                    
                                    <asp:TemplateField HeaderText="Total " Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttotal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total ")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
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
            
       </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>



