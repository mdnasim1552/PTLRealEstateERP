<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptUpconVsSobCon.aspx.cs" Inherits="RealERPWEB.F_16_Bill.RptUpconVsSobCon" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
  
   
     <style type="text/css">
        .tblh {
            background: #DFF0D8;
            height: 30px;
            text-align: center;
        }

        .th1 {
            width: 170px;
            text-align: center;
        }

        .th2 {
            width: 60px;
            text-align: center;
        }

        .th3 {
            width: 60px;
            text-align: center;
        }
         .th4 {
            width: 60px;
            text-align: center;
        }

        .th5 {
            width: 60px;
            text-align: center;
        }
        #monsale {
            margin-left: 3px;
        }
         #yearsale {
             margin-left: 3px;
         }
    </style>
    

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

          
            $('#<%=this.gvSubBill.ClientID%>').gridviewScroll({
                width: 1170,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 14


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
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server" Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" CssClass="ddlistPull"
                                            AutoPostBack="True">
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                        
                                    </div>
                                    
                                </div>

                        <div class="form-group">
                            <div class="col-md-4 pading5px asitCol4">

                                <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                    Text="From.:" CssClass="lblTxt lblName"></asp:Label>

                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox" ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                <asp:Label ID="Label6" runat="server"
                                    Text="To:" CssClass="smLbl_to"></asp:Label>

                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" ClientIDMode="Static"
                                   ></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                
                            </div>
                        </div>

                    </div>

                </fieldset>
                      

                <%--OnRowCreated="gvSubBill_RowCreated" OnRowDataBound="gvSubBill_RowDataBound"--%>
                     <asp:GridView ID="gvSubBill" runat="server"
                                AutoGenerateColumns="False"
                               PageSize="20"
                                ShowFooter="True" Width="752px" CssClass=" table-responsive table-striped table-hover table-bordered grvContentarea"  >
                                <PagerSettings PageButtonCount="20" Mode="NumericFirstLast"  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>

                                  
                                    <asp:TemplateField HeaderText="Work Description">
                                       

                                         <FooterTemplate>
                                            <asp:Label ID="lblgvFTotal" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px" Text="Total"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>

                                            <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"                                              
                                                
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                  Width="280px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MB Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmajbook" runat="server" Font-Bold="False" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "majbook")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Page Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmajpnumber" runat="server" Font-Bold="False" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "majpnumber")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   
                                     <asp:TemplateField HeaderText="Subcon<br> Rate">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Upcon <br>Rate">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvuprate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uprate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                   




                                       <asp:TemplateField HeaderText=" Subcon  Qty <br> for this R/A">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText=" Upcon Qty <br> for this R/A ">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvupconqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "upconqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     
                                       


                                      <asp:TemplateField HeaderText=" Subcon Amount <br> for this R/A">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFconamt" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conamt")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Upcon Amount <br> for this R/A">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFupconamt" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvupconamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "upconamt")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    


                                      <asp:TemplateField HeaderText=" Subcon Cumulative Qty">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFconuqty" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconuqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conuqty")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Upcon  Cumulative Qty">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFupconuqty" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvupconuqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "upconuqty")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                   

                                       <asp:TemplateField HeaderText="Quantity<br> Difference">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFdifamt" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lbldifamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difupacon")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

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

