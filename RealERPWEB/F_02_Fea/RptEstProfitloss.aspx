<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEstProfitloss.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptEstProfitloss" %>
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

    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">

                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                <div class="colMdbtn">
                                    <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-5 pading5px ">
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="3">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectdesc" runat="server"
                                    Visible="False" CssClass="form-control inputTxt"></asp:Label>

                            </div>

                            <div class="col-md-1 pading5px">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>

                            </div>
                            <div class="col-md-3 pull-right pading5px">
                                <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                            </div>

                        </div>
                    </div>
                    <asp:Panel ID="PanelSelName" runat="server" Visible="False">

                      

                    </asp:Panel>
                </fieldset>
            </div>
            <div class="row">

           
                   
                        <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
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
                                <asp:TemplateField HeaderText="Description">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotalproinfo"
                                            runat="server"
                                            OnClick="llbtnTotalproinfo_Click" CssClass="btn  btn-primary  primarygrdBtn">Total</asp:LinkButton>

                                        <asp:LinkButton ID="Calculation"
                                            runat="server"
                                            OnClick="llbtnCalculation_Click" CssClass="btn  btn-primary  primarygrdBtn">Calculation</asp:LinkButton>
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
                              <%--  <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lgp" runat="server"
                                            Font-Bold="True" Font-Size="12px" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="4px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Type" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdatProInfo"
                                            runat="server"
                                            OnClick="lUpdatProInfo_Click" CssClass="btn btn-danger primarygrdBtn">Update Information</asp:LinkButton>
                                    </FooterTemplate>
                                  <%--  <ItemTemplate>
                                        <asp:TextBox
                                            ID="txtgvVal" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>'
                                            Width="300px"></asp:TextBox>

                                        <asp:TextBox
                                            ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>'
                                            Width="300px"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender_txtgvdVal" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                        <asp:DropDownList ID="ddlcataloc" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                    </ItemTemplate>--%>

                                    <HeaderStyle
                                        HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Estimated Cost">

                                    <ItemTemplate>
                                        <asp:TextBox
                                            ID="txtestcost" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estcost")).ToString("#,#0; (#,##0); ") %>'
                                            Width="80px"></asp:TextBox>


                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Actual Cost">

                                    <ItemTemplate>
                                        <asp:TextBox
                                            ID="txtbuiactual" runat="server" BackColor="Transparent" BorderStyle="none" CssClass="txtAlgRight"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actual")).ToString("#,#0; (#,##0); ") %>'
                                            Width="80px"></asp:TextBox>


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


                        <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                        <h4 class="modal-title" id="myModalLabel">Project Image preview</h4>
                                    </div>
                                    <div class="modal-body">
                                        <img src="" id="imagepreview" class="img img-responsive">
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
              

            </div>


        </div>
    </div>
 <%--   <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>--%>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
