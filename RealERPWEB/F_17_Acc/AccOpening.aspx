<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccOpening.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('#<%=this.txtFilter.ClientID %>').focus();

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            

           <%-- var gv1 = $('#<%=this.dgv3.ClientID %>');


             gv1.gridviewScroll({
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




        };

        function showwindow(data) {
            //alert("Hello  docu");
            window.open(data, '_blank');
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
                                        <asp:Label ID="lblopndate" runat="server" CssClass="lblTxt lblName">Opening Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox" TabIndex="0"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>

                                     <div class="col-md-3 pading5px pull-right">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar" Text="Please wait......"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>

                                   

                                    
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px ">
                                        <asp:Label ID="lblacccode1" runat="server" CssClass="lblTxt lblName">Accounts Code</asp:Label>
                                        <asp:TextBox ID="txtFilter" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImageButton1" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImageButton1_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                  
                                    
                                    

                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <asp:Panel ID="pnlxcel" runat="server">
                                            <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exele :"></asp:Label>
                                            <div class="uploadFile">
                                                <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                            </div>

                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <asp:LinkButton ID="btnexcuplosd" runat="server" CssClass=" btn btn-danger primarygrdBtn" Text="Upload Exel" OnClick="btnexcuplosd_Click"></asp:LinkButton>
                                    </div>


                                    <div class="col-md-3 pading5px ">
                                            <asp:Label ID="lblpagem" runat="server" CssClass=" smLbl_to" Text="Page"></asp:Label>

                                            <asp:DropDownList ID="ddlpagem" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagem_SelectedIndexChanged" TabIndex="3">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                                <asp:ListItem>600</asp:ListItem>
                                                <asp:ListItem>900</asp:ListItem>
                                                <asp:ListItem>1200</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                                        <div class="col-md-3">
                                     <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Report Type"></asp:Label>

                                            <asp:DropDownList ID="ddreportType" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagem_SelectedIndexChanged" TabIndex="3">
                                                <asp:ListItem>Summary</asp:ListItem>
                                                 <asp:ListItem>Details</asp:ListItem>
         
                                            </asp:DropDownList>
                                                            
                                        </div>


<%--                                    <div class="col-md-2 ">
                                        <a id="BtnLink" runat="server">Help</a>
                                        <asp:LinkButton ID="lnkbtnHelp" runat="server" CssClass="btn btn-danger primarygrdBtn pull-right" OnClick="lnkbtnHelp_Click">Documents</asp:LinkButton>
                                    </div>--%>

                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="row">

                        <asp:GridView ID="dgv2" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnRowCreated="dgv2_RowCreated"
                            PagerSettings-Position="Bottom"
                           
                            RowStyle-Font-Size="12px" ShowFooter="True"
                            Width="600px" OnRowCommand="dgv2_RowCommand" PageSize="15" CssClass="table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="dgv2_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtndelete" runat="server" CssClass="btn btn-xs btn-danger" OnClick="lbtndelete_Click"><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                 <%--   <FooterTemplate>
                                       
                                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" 
                                                    OnClick="lnkFinalUpdate_Click"
                                                    CssClass="btn btn-danger primarygrdBtn">Final Update</asp:LinkButton></li>
                                           
                                     
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Font-Size="11px" Width="400px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level"
                                    ItemStyle-HorizontalAlign="Center">
                                    <%--<FooterTemplate>
                                        <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total :</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                            onmouseover="style.color='#FF9999'" onmouseout="style.color='blue'"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="50px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Width="103px" Font-Bold="True" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    
                                      <asp:Label ID="lblgvFDr" runat="server" BackColor="Transparent" ForeColor="Red" ToolTip="Difference"
                                            BorderColor="Transparent" BorderStyle="None" Width="103px" Font-Bold="True"  Style="text-align: right"></asp:Label>
                                    
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Width="103px" ReadOnly="True" Font-Bold="True" Style="text-align: right"></asp:TextBox>

                                         <asp:Label ID="lblgvFCr" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" ForeColor="Red" ToolTip="Difference"
                                            Width="103px" ReadOnly="True" Font-Bold="True" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Remarks"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtmaingvrmks" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "opnnar")) %>'
                                                Width="180px" Font-Size="12px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>



                    <asp:Panel ID="pnlsub" runat="server">

                        <div class="row">

                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">

                                    <div class="form-group">

                                        <asp:Label ID="lblacccode2" runat="server" Font-Bold="True"
                                            Font-Names="Verdana" Font-Size="16px" Text="Resource Entry Screen"></asp:Label>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px ">
                                            <asp:Label ID="lblacccode" runat="server" CssClass="lblTxt lblName">Accounts Code</asp:Label>
                                            <asp:TextBox ID="txtActcode" runat="server" CssClass=" form-control inputTxt" Width="450px" ReadOnly="true"></asp:TextBox>

                                        </div>


                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="3">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                                <asp:ListItem>900</asp:ListItem>
                                                <asp:ListItem>1200</asp:ListItem>
                                                <asp:ListItem>1500</asp:ListItem>
                                                <asp:ListItem>3000</asp:ListItem>
                                                <asp:ListItem>6000</asp:ListItem>
                                                <asp:ListItem>9000</asp:ListItem>
                                            </asp:DropDownList>




                                        </div>

                                        <div class="col-md-1  pull-right">
                                            <div class="colMdbtn">
                                                <%--<asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkSubmit_Click">Home</asp:LinkButton>--%>

                                            </div>
                                        </div>
                    

              
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Resource Code</asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>


                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImageButton2" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImageButton2_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-3 pading5px pull-right">
                                            <div class="msgHandSt">
                                                <asp:Label ID="lblmsg01" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                            </div>


                                        </div>

                                    </div>


                                </div>


                            </fieldset>



                            <asp:GridView ID="dgv3" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging"
                                ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDeleting="dgv3_RowDeleting">
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblrescode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res.Description"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>

                                               <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"  CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>

                                        </HeaderTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblresunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="40px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Specification"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblgvspecification" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" Font-Bold="True" CssClass="btn btn-success btn-xs  primarygrdBtn"
                                                OnClick="lnkbtnUpdateRes_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="gvlnkFTotal" runat="server" CssClass="btn btn-primary  btn-xs   primarygrdBtn" OnClick="gvlnkFTotal_Click">Total 
                                                                    :</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr. Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                Font-Bold="True"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Width="116px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Width="106px" Style="text-align: right"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Remarks"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtgvremarks" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                Width="180px" Font-Size="12px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>





                        </div>




                    </asp:Panel>




                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

