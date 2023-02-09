<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMatIssueStatus.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptMatIssueStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

 
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded()

        {

            try {
                

                var comcod =<%=this.GetCompCode()%>;
                switch (comcod) {
                    case 3370://CPDL                
                    case 3101://PTL

                        $('#<%=this.lblsisasirno.ClientID%>').text("SIS/SIR No");
                        break;


                    default:

                        break;
                }



                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });
                $('.select2').each(function () {
                    var select = $(this);
                    select.select2({
                        placeholder: 'Select an option',
                        width: '100%',
                        allowClear: !select.prop('required'),
                        language: {
                            noResults: function () {
                                return "{{ __('No results found') }}";
                            }
                        }
                    });
                });
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e)
            {
                alert(e.message);

            }
        }
    </script>
 

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;}
    </style>



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
            <div class="card mt-2">
                <div class="card-header">
                    <div class="row mt-4 mb-2">
                        
                                 <div class="col-md-1">
                                        <asp:Label ID="lbldate" runat="server" CssClass="form-label" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                    </div>
                        <div class="col-md-1">
                            <asp:Label ID="lbltodate" runat="server" CssClass="form-label" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                         <div class="col-md-3">
                                        <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Project Name"></asp:Label>
                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="chzn-select  form-control form-control-sm" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>
                        <div class="col-md-2 d-none">
                                <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="lbtnresource" runat="server"  TabIndex="2">Material&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                              <asp:ListBox ID="chkResourcelist" runat="server" CssClass="form-control select2 form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                        </div>
                        <div class="col-md-2">
                           
                            
                                <asp:Label ID="lblresName" runat="server" CssClass="control-label" Text="Matrial Name"></asp:Label>
                             <asp:DropDownList ID="ddlmatlist" runat="server" CssClass="chzn-select  form-control form-control-sm" TabIndex="3">
                                        </asp:DropDownList>
                              
                           
                        
                        </div>
                                   
                                    <div class="col-md-2">
                                        <asp:Label ID="lblsisasirno" runat="server" CssClass="form-label" Text="SMCR No/ DMIRF No"></asp:Label>
                                        <asp:TextBox ID="txtSrcRefNo" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>

                                       
                                    </div>
                                      <div class="col-md-1" style="margin-top:21px;">
                                            <asp:LinkButton ID="imgbtnFindRefno" CssClass="btn btn-primary btn-sm" runat="server" OnClick="imgbtnFindRefno_Click" TabIndex="2"><span class="fa fa-search"> </span></asp:LinkButton>
                                        </div>

                         <div class="col-md-1" style="margin-top:21px;">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click" TabIndex="4">ok</asp:LinkButton>

                                    </div>
                                <div class="col-md-1">
                                        <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                <div class="form-group d-none">

                                    <div class="col-md-3 pading5px asitCol3">
                                        
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                   


                                </div>

                               

                           
                    </div>
                    </div>
                 <div class="table-responsive">

                <div class="card-header">
                   
                        <asp:GridView ID="gvMatIssueStatus" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvMatIssueStatus_PageIndexChanging">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgcProName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue No">
                                <ItemTemplate>
                                    <asp:Label ID="lgcIsuNo" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno1")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SMCR. No">
                                <ItemTemplate>
                                    <asp:Label ID="lgcissuerefno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isurefno")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="DMIRF. No">
                                <ItemTemplate>
                                    <asp:Label ID="lgcmdirfno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dmirfno")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Issue Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgcIsuDat" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat1")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Materials Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcMatDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgcUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lgcScfcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Qty">
                                 <FooterTemplate>
                                                    <asp:Label ID="lblissueqty" runat="server"  Width="80px" Font-Bold="True" Font-Size="12px"
                                                         Style="text-align: right" >Total</asp:Label>
                                         </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvBgdQty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             

                             <asp:TemplateField HeaderText="Use of Location">
                                <ItemTemplate>
                                    <asp:Label ID="lgvlocation" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "useoflocation")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Issue Rate" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBgdrate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avragerate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Issue Amount" Visible="false">
                                 <FooterTemplate>
                                                    <asp:Label ID="lblIssueAmount" runat="server"  Width="90px" Font-Bold="True" Font-Size="12px"
                                                         Style="text-align: right" ></asp:Label>
                                         </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvBgdamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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




