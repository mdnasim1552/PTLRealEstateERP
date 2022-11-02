<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEstProfitloss.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptEstProfitloss" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            //$(".pop").on("click", function () {
            //    $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
            //    $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            //});
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card mt-4">
                <div class="card-body">
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
                        

                                    <div class="col-md-3 pading5px asitCol3 d-none">
                                       
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                         <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-2">
                                       
                                        <asp:Label ID="lblCurDate" runat="server" CssClass="form-label" Text="Date"></asp:Label>

                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                    </div>
                         
                        <div class="col-md-1" style="margin-top:22px;">
                             <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>
                        </div>
                                    <div class="col-md-2 pull-right pading5px d-none">
                                        <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                        </div>
                     <div class="row mt-2 mb-2">
                              
                            <asp:Panel ID="PanelSelName" runat="server" Visible="false">
                                
                                    
                                        <div class="col-md-12" style="font-weight:bold; font-size:12px;">
                                            <asp:Label ID="lblUnitNameid" runat="server" CssClass="form-label" Text="Unit Name :"></asp:Label>
                                            <asp:Label ID="lblUnitName" runat="server" CssClass="form-label"></asp:Label>
                                            <asp:Label ID="lblunitsize" runat="server" CssClass="form-label" Text="Size :"></asp:Label>
                                            <asp:Label ID="lblunitsizeval" runat="server" CssClass="form-label"></asp:Label>

                                            <asp:Label ID="lblrate1" runat="server" CssClass="form-label" Text="Rate :"></asp:Label>
                                            <asp:Label ID="lblrate" runat="server" CssClass="form-label" ></asp:Label>


                                            <asp:Label ID="lblpurdate" runat="server" Text="Purchase Date :" CssClass="form-label"></asp:Label>
                                            <asp:Label ID="lblpurdate1" runat="server" CssClass="form-label" Width="100px"></asp:Label>



                                            <asp:Label ID="lblPurValuse" runat="server" Text="Purchase Value :" CssClass="form-label"></asp:Label>
                                            <asp:Label ID="lblPurValuse1" runat="server" CssClass="form-label" Font-Size="13px"></asp:Label>



                                            <asp:Label ID="Label20" runat="server" CssClass="form-label ml-2" Text="Target Sales Value/Price:" Width="130px"></asp:Label>

                                            <asp:Label ID="lblcommitedval" runat="server" CssClass="form-label" Font-Size="13px"></asp:Label>

                                    <asp:Label ID="lblactualsal" runat="server" CssClass="form-label ml-2" Text="Today Sales Value/Price:" Width="130px"></asp:Label>

                                            <asp:Label ID="lblactualsal1" runat="server" CssClass="form-label" Font-Size="13px"></asp:Label>


                                        </div>

                                        <%--   <div class="col-md-3 pading5px">

                                <asp:Label ID="lblPurValuse" runat="server" Text="Purchase Value" CssClass="lblTxt lblName"></asp:Label>
                                   
                                <asp:Label ID="lblPurValuse22" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                    

                            </div>--%>

                                        <%--<div class="col-md-2 pading5px">
                                </div>--%>
                                    

                            </asp:Panel>
                       
                    </div>
                    </div>
                </div>
             <div class="card mt-4" style="min-height:480px;">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-8">
                            <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvProjectInfo_RowDataBound">
                                <RowStyle Font-Size="11px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0"
                                                runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lblgvItmCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estgcod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">

                                        <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description" Width="120px"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel"></i>
                                            </asp:HyperLink>
                                        </HeaderTemplate>


                                        <FooterTemplate>
                                            <%--<asp:LinkButton ID="lbtnTotalproinfo"
                                    runat="server"
                                    OnClick="llbtnTotalproinfo_Click" CssClass="btn  btn-primary  primarygrdBtn">Total</asp:LinkButton>--%>

                                            <asp:LinkButton ID="Calculation"
                                                runat="server"
                                                OnClick="llbtnCalculation_Click" CssClass="btn btn-sm  btn-primary  primarygrdBtn">Calculation</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lgcResDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estgdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True"
                                            HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center"
                                            VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <%--               <asp:TemplateField HeaderText="Type" Visible="False">
                            <ItemTemplate>
                                <asp:Label
                                    ID="lgvgval" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>


                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:TextBox
                                                ID="txtcostoffund" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fundamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:TextBox>


                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lUpdatProInfo"
                                                runat="server"
                                                OnClick="lUpdatProInfo_Click" CssClass="btn btn-sm btn-danger primarygrdBtn">Update</asp:LinkButton>
                                        </FooterTemplate>

                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Percent (%)">

                                        <ItemTemplate>
                                            <asp:TextBox
                                                ID="txtpercnt" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="45px"></asp:TextBox>


                                        </ItemTemplate>

                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Estimated /</br>Forcusted Cost">

                                        <ItemTemplate>
                                            <asp:TextBox
                                                ID="txtestcost" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estcost")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:TextBox>


                                        </ItemTemplate>

                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Cost</br> as on today">

                                        <ItemTemplate>
                                            <asp:TextBox
                                                ID="txtbuiactual" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actual")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:TextBox>


                                        </ItemTemplate>

                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Saving/Excess">

                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lblsaving" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>


                                        </ItemTemplate>

                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Date">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDate" runat="server" Font-Size="11px"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paymentdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paymentdate")).ToString("dd-MMM-yyyy")) %>'
                                                Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                        </ItemTemplate>

                                        <HeaderStyle
                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                        <div class="col-md-4">

                            <asp:GridView ID="gvAgeing" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False">

                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText="Ageing">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdesc" runat="server" Style="text-align: left" Font-Bold="true" Font-Size="14px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="14px" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lgnaginday" runat="server" Style="text-align: right" Font-Size="14px" Font-Bold="true"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aginday")) %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFaginday" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle Font-Bold="true" Font-Size="14px" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdate" runat="server" Style="text-align: right" Font-Size="12px" Font-Bold="true"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "efectivedate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "efectivedate")).ToString("dd-MMM-yyyy")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFgvdate" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle Font-Bold="true" Font-Size="14px" />

                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                        <div class="col-md-4">
                            <asp:Label ID="lblsaleprice" runat="server" Text="Break even sales price on Aeging  :" Width="140px" ForeColor="#ff3300" Font-Size="14px" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                            <asp:Label ID="lblsalecore" runat="server" CssClass="smLbl" Font-Bold="true" Font-Size="16px" Width="120px" Font-Underline="true" ForeColor="#cc3399" Visible="false"></asp:Label>
                            <asp:Label ID="lblsalecoreactual" runat="server" CssClass="smLbl" Font-Bold="true" Font-Size="16px" Width="120px" Font-Underline="true" ForeColor="#cc3399"></asp:Label>



                        </div>



                    </div>


                    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                    <h4 class="modal-title" id="myModalLabel">Project Image preview</h4>
                                </div>
                                <div class="modal-body">
                                    <img src="" id="imagepreview" class="img img-responsive" />
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
