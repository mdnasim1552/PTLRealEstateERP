<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SaleSurvey.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SaleSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .lnkbtmfromtop {
            margin-top: 28px;
            margin-left: 5px;
        }

        .srchbtmfromtop {
            margin-top: 5px !important;
        }
        .grvContentarea {}
    </style>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $(".select2").select2();

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

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
            var gridview = $('#<%=this.gvMSRInfo2.ClientID %>');
            $.keynavigation(gridview);
        }
    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>




            <div class="card card-fluid mb-2">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Survey No</label>
                                <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="form-control form-control-sm"></asp:Label>


                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Date</label>
                                <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass=" form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>


                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label id="lblprevious" runat="server">
                                    Previous
                                    <asp:LinkButton ID="lnkbtnFindPreMR" runat="server" OnClick="lnkbtnFindPreMR_Click"> <i class="fa fa-search" aria-hidden="true"></i>
                                    </asp:LinkButton></label>

                                <asp:DropDownList ID="ddlPrevMSRList" runat="server" CssClass="form-control form-control-sm  chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:LinkButton ID="lbtnMSROk" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lbtnMSROk_Click">Ok</asp:LinkButton>

                            </div>

                        </div>

                    </div>

                </div>


            </div>

            <div class="card card-fluid  mb-2">
                <div class="row">


                    <div class="col-md-3">
                        <div class="from-group">
                            <label id="lblprojectname" runat="server" visible="false">Project Name</label>
                            <asp:ListBox ID="chkProjectName" runat="server" ClientIDMode="Static" CssClass="form-control form-control-sm  select2" Style="min-width: 200px !important;" SelectionMode="Multiple" Visible="false"></asp:ListBox>

                        </div>
                    </div>

                    <div class="col-md-1">
                        <div class="form-group">

                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lbtnSelect_Click" Visible="false">Select</asp:LinkButton>

                        </div>

                    </div>






                </div>

            </div>




            <asp:GridView ID="gvMSRInfo2" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" ShowFooter="True"
                OnRowDataBound="gvMSRInfo2_RowDataBound" OnRowCreated="gvMSRInfo2_RowCreated" Width="1225px">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


<%--                    btypecode, location, aptsize, landarea,  storied,  aptunit, askingprice, selprice, utlityprice, prkingpirce, hoverdate, procatagory, 
	buildtype, pactdesc,  companycode, companyname --%>


                    <asp:TemplateField HeaderText=" Developer">

                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary  primarygrdBtn">Total</asp:LinkButton>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lblgvdeveloper" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))   %>'
                                Width="120px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    
                    <asp:TemplateField HeaderText="Building Type">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvbuildtype" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "buildtype"))   %>'
                                Width="120px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Location">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvLocation" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                Width="150px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Apt. Size(sft)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvaptsize" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "aptsize"))   %>'
                                Width="120px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                     <asp:TemplateField HeaderText="Land Area(katha)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvaptsize" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "landarea")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="80px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="  Storied">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvstoried" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "storied"))   %>'
                                Width="120px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                  
                     <asp:TemplateField HeaderText="Total Apt. Units.(Nos)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvaptsize" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptunit")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    
                     <asp:TemplateField HeaderText="Asking Price(TK/sft)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvaskingprice" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "askingprice")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Selling Price(TK/sft)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvselprice" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "selprice")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Utility Price(TK in Lac)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvutlityprice" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utlityprice")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Parking Price(TK in Lac)">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvprkingpirce" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prkingpirce")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                   
                    
                    <asp:TemplateField HeaderText="Handover Date">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvstoried" runat="server"
                                Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "hoverdate")).ToString("dd-MMM-yyyy")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                     <asp:TemplateField HeaderText="Catagory">                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvstoried" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "procatagory"))   %>'
                                Width="100px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>



                   
                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>



            <div class="row">
                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <asp:Label ID="lblReqNarr" runat="server" Text="Narration:" CssClass="lblName lblTxt"></asp:Label>
                    <asp:TextBox ID="txtMSRNarr" runat="server" Width="322px" CssClass="inputtextbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </asp:Panel>
            </div>







        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

